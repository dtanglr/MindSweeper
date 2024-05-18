using MediatR;
using SchneiderElectric.MindSweeper.Application.Components;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public class MoveCommandHandler(IGameRepository repository) : IRequestHandler<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result<MoveCommandResponse>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getGameResult = await _repository.GetGameAsync(request.PlayerId, cancellationToken);

            if (!getGameResult.IsSuccess)
            {
                return getGameResult.ToResult<MoveCommandResponse>();
            }

            var game = getGameResult.Value!;
            var settings = game.Settings;
            var squares = new Field.Squares(settings);
            var fromSquare = squares[game.CurrentSquare];

            if (!fromSquare.TryMove(request.Direction, out var toSquare))
            {
                return Result<MoveCommandResponse>.Unprocessable();
            }

            var bombs = new Field.Bombs(game.Bombs);
            var hitBomb = toSquare!.HasBomb(bombs);
            var lives = hitBomb ? game.Lives - 1 : game.Lives;
            var moves = game.Moves + 1;
            var availableMoves = toSquare.GetAvailableMoves();
            var updatedGame = game with
            {
                CurrentSquare = toSquare.Name,
                AvailableMoves = availableMoves,
                Lives = lives,
                Moves = moves
            };

            if (updatedGame.Status == GameStatus.InProgress)
            {
                var updateResult = await _repository.UpdateGameAsync(updatedGame, cancellationToken);

                if (!updateResult.IsSuccess)
                {
                    return updateResult.ToResult<MoveCommandResponse>();
                }
            }
            else
            {
                var deleteResult = await _repository.DeleteGameAsync(request.PlayerId, cancellationToken);

                if (!deleteResult.IsSuccess)
                {
                    return deleteResult.ToResult<MoveCommandResponse>();
                }
            }

            var response = new MoveCommandResponse(updatedGame, fromSquare.Name, toSquare.Name, request.Direction, hitBomb);

            return Result<MoveCommandResponse>.Accepted(response);
        }
        catch (Exception ex)
        {
            return Result<MoveCommandResponse>.Error(ex.Message);
        }
    }
}
