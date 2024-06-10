using MindSweeper.Cli.Commands.End;
using MindSweeper.Cli.Properties;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.UnitTests.CommandTests.End;

public class EndCommandViewTests
{
    private readonly IGameConsole _console;

    public EndCommandViewTests()
    {
        _console = new GameTestConsole();
    }

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
