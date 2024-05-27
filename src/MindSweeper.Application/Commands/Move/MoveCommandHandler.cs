using MediatR;
using MindSweeper.Domain;
using MindSweeper.Domain.Components;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Handles the MoveCommand and updates the game state accordingly.
/// </summary>
public class MoveCommandHandler : IRequestHandler<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The game repository.</param>
    public MoveCommandHandler(IGameRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the MoveCommand and updates the game state accordingly.
    /// </summary>
    /// <param name="request">The MoveCommand request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the MoveCommand handling.</returns>
    public async Task<Result<MoveCommandResponse>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var game = request.Game!;
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
