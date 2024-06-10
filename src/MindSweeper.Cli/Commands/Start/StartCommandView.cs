using MindSweeper.Application.Mediator.Commands.Start;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents the view for starting the game.
/// </summary>
internal class StartCommandView : ICommandView<Result<StartCommandResponse>>
{
    private readonly IGameConsole _console;
    private readonly ErrorView _errorView;
    private readonly GameStatusView _gameStatusView;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandView"/> class.
    /// </summary>
    /// <param name="console">The game console.</param>
    public StartCommandView(IGameConsole console)
    {
        _console = console;
        _errorView = new(_console);
        _gameStatusView = new(_console);
    }

    /// <summary>
    /// Renders the result of the start command.
    /// </summary>
    /// <param name="result">The result of the start command.</param>
    public void Render(Result<StartCommandResponse> result)
    {
        _console.WriteLine();

        switch (result.Status)
        {
            case ResultStatus.Accepted:
                var game = result.Value!.Game;
                _console.WriteLine(Resources.StartCommandResultStatusAccepted);
                _console.WriteLine();
                _gameStatusView.Render(game);
                break;
            case ResultStatus.Conflict:
                _console.WriteLine(Resources.StartCommandResultStatusConflict);
                break;
            default:
                _errorView.Render(result.ToResult());
                break;
        }

        _console.WriteLine();
    }
}
