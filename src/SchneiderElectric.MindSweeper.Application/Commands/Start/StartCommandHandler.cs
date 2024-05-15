using MediatR;
using SchneiderElectric.MindSweeper.Application.Components;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public class StartCommandHandler(IGameRepository repository) : IRequestHandler<StartCommand, Result<StartCommandResponse>>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result<StartCommandResponse>> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var playerId = request.PlayerId;
        var settings = request.Settings;
        var lives = request.Settings.Lives;
        var bombs = new Field.Bombs(settings);
        var squares = new Field.Squares(settings);
        var startSquare = squares.GetStartSquare();
        var availableMoves = startSquare
            .GetAvailableMoves()
            .Select(m => new KeyValuePair<Direction, string>(m.Key, m.Value.Name))
            .ToDictionary();

        var game = new Game(id, playerId, settings, bombs, lives, 0, startSquare.Name, availableMoves);
        var result = await _repository.CreateGameAsync(game, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ToResult<StartCommandResponse>();
        }

        var response = new StartCommandResponse(game);

        return Result<StartCommandResponse>.Accepted(response);
    }
}
