using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Views;

/// <summary>
/// Represents the view for displaying the end result of a game.
/// </summary>
internal class EndCommandView : IResultView
{
    private readonly IGameConsole _console;
    private readonly ErrorView _errorView;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandView"/> class.
    /// </summary>
    /// <param name="console">The game console used for output.</param>
    public EndCommandView(IGameConsole console)
    {
        _console = console;
        _errorView = new(_console);
    }

    /// <summary>
    /// Renders the end result of a game.
    /// </summary>
    /// <param name="result">The result of the game.</param>
    public void Render(Result result)
    {
        _console.WriteLine();

        switch (result.Status)
        {
            case ResultStatus.Accepted:
                _console.WriteLine(Resources.EndCommandResultStatusAccepted);
                break;
            case ResultStatus.NotFound:
                _console.WriteLine(Resources.EndCommandResultStatusNotFound);
                break;
            default:
                _errorView.Render(result);
                break;
        }

        _console.WriteLine();
    }
}
