using MindSweeper.Application.Mediator.Commands.Start;
using MindSweeper.Cli.Commands.Start;

namespace MindSweeper.Cli.UnitTests.CommandTests.Start;

/// <summary>
/// Unit tests for the StartCommandView class.
/// </summary>
public class StartCommandViewTests
{
    private readonly IGameConsole _console;
    private readonly Fixture _fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandViewTests"/> class.
    /// </summary>
    public StartCommandViewTests()
    {
        _console = new GameTestConsole();
        _fixture = new Fixture();
    }

    /// <summary>
    /// Test case to verify that the Render method displays the accepted message when the result is Accepted.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_Message()
    {
        // Arrange
        var view = new StartCommandView(_console);
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .Create();
        var response = new StartCommandResponse(game);
        var result = Result<StartCommandResponse>.Accepted(response);

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.StartCommandResultStatusAccepted);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the conflict message when the result is Conflict.
    /// </summary>
    [Fact]
    public void Render_Conflict_Result_Should_Display_Conflict_Message()
    {
        // Arrange
        var view = new StartCommandView(_console);
        var result = Result<StartCommandResponse>.Conflict();

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.StartCommandResultStatusConflict);
    }
}
