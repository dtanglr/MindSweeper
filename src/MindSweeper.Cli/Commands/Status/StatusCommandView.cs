using MindSweeper.Application.Mediator.Queries.GetGame;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Commands.Status;

/// <summary>
/// Represents a view for displaying the status of a game.
/// </summary>
internal class StatusCommandView : ICommandView<Result<GetGameQueryResponse>>
{
    private readonly IGameConsole _console;
    private readonly ErrorView _errorView;
    private readonly GameStatusView _gameStatusView;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCommandView"/> class.
    /// </summary>
    /// <param name="console">The game console.</param>
    public StatusCommandView(IGameConsole console)
    {
        _console = console;
        _errorView = new(_console);
        _gameStatusView = new(_console);
    }

    /// <summary>
    /// Renders the result of the GetGameQuery.
    /// </summary>
    /// <param name="result">The result of the GetGameQuery.</param>
    public void Render(Result<GetGameQueryResponse> result)
    {
        _console.WriteLine();

        switch (result.Status)
        {
            case ResultStatus.Ok:
                var game = result.Value!.Game;
                _console.WriteLine(Resources.StatusCommandResultStatusOk);
                _console.WriteLine();
                _gameStatusView.Render(game);
                break;
            case ResultStatus.NotFound:
                _console.WriteLine(Resources.StatusCommandResultStatusNotFound);
                break;
            default:
                _errorView.Render(result.ToResult());
                break;
        }

        _console.WriteLine();
    }
}
