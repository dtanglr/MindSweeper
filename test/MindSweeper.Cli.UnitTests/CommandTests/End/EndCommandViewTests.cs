using MindSweeper.Cli.Commands.End;

namespace MindSweeper.Cli.UnitTests.CommandTests.End;

/// <summary>
/// Represents the unit tests for the EndCommandView class.
/// </summary>
public class EndCommandViewTests
{
    private readonly IGameConsole _console;

    /// <summary>
    /// Initializes a new instance of the EndCommandViewTests class.
    /// </summary>
    public EndCommandViewTests()
    {
        _console = new GameTestConsole();
    }

    /// <summary>
    /// Tests the Render method of EndCommandView with an Accepted result.
    /// It should display the accepted message.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_Message()
    {
        // Arrange
        var view = new EndCommandView(_console);
        var result = Result.Accepted();

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.EndCommandResultStatusAccepted);
    }

    /// <summary>
    /// Tests the Render method of EndCommandView with a NotFound result.
    /// It should display the not found message.
    /// </summary>
    [Fact]
    public void Render_NotFound_Result_Should_Display_NotFound_Message()
    {
        // Arrange
        var view = new EndCommandView(_console);
        var result = Result.NotFound();

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.EndCommandResultStatusNotFound);
    }
}
