using MediatR;
using MindSweeper.Application.Components;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

public class StartCommandHandler(IGameRepository repository) : IRequestHandler<StartCommand, Result<StartCommandResponse>>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result<StartCommandResponse>> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = Guid.NewGuid();
            var playerId = request.PlayerId;
            var settings = request.Settings;
            var squares = new Field.Squares(settings);
            var startSquare = squares.GetStartSquare();
            var availableMoves = startSquare.GetAvailableMoves();
            var bombs = new Field.Bombs(settings);
            var lives = settings.Lives;
            var game = new Game(id, playerId, settings, bombs, lives, 0, startSquare.Name, availableMoves);
            var result = await _repository.CreateGameAsync(game, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToResult<StartCommandResponse>();
            }

            var response = new StartCommandResponse(game);

            return Result<StartCommandResponse>.Accepted(response);
        }
        catch (Exception ex)
        {
            return Result<StartCommandResponse>.Error(ex.Message);
        }
    }
}
