using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Commands;

/// <summary>
/// Represents a view for displaying error results.
/// </summary>
internal class ErrorView : ICommandView<Result>
{
    private readonly IGameConsole _console;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorView"/> class.
    /// </summary>
    /// <param name="console">The game console used for rendering.</param>
    public ErrorView(IGameConsole console)
    {
        _console = console;
    }

    /// <summary>
    /// Renders the specified error result.
    /// </summary>
    /// <param name="result">The error result to render.</param>
    public void Render(Result result)
    {
        switch (result.Status)
        {
            case ResultStatus.Invalid:
                _console.WriteLine(Resources.CommandResultStatusInvalid);
                result.ValidationIssues.ForEach(e => _console.WriteLine(e.Message));
                break;
            case ResultStatus.Error:
                _console.WriteLine(Resources.CommandResultStatusError);
                result.Errors.ForEach(_console.WriteLine);
                break;
            default:
                _console.WriteLine(Resources.CommandResultStatusUnhandled, result.Status);
                break;
        }
    }
}
