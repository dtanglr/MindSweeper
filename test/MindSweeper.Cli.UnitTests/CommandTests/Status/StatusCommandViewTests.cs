using MindSweeper.Application.Mediator.Queries.GetGame;
using MindSweeper.Cli.Commands.Status;

namespace MindSweeper.Cli.UnitTests.CommandTests.Status;

/// <summary>
/// Represents the unit tests for the StatusCommandView class.
/// </summary>
public class StatusCommandViewTests
{
    private readonly IGameConsole _console;
    private readonly Fixture _fixture;

    /// <summary>
    /// Initializes a new instance of the StatusCommandViewTests class.
    /// </summary>
    public StatusCommandViewTests()
    {
        _console = new GameTestConsole();
        _fixture = new Fixture();
    }

    /// <summary>
    /// Tests the Render method of StatusCommandView with an Accepted result.
    /// It should display the accepted message.
    /// </summary>
    [Fact]
    public void Render_Ok_Result_Should_Display_Ok_Message()
    {
        // Arrange
        var view = new StatusCommandView(_console);
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .Create();
        var response = new GetGameQueryResponse(game);
        var result = Result<GetGameQueryResponse>.Success(response);

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.StatusCommandResultStatusOk);
    }

    /// <summary>
    /// Tests the Render method of StatusCommandView with a NotFound result.
    /// It should display the not found message.
    /// </summary>
    [Fact]
    public void Render_NotFound_Result_Should_Display_NotFound_Message()
    {
        // Arrange
        var view = new StatusCommandView(_console);
        var result = Result<GetGameQueryResponse>.NotFound();

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.StatusCommandResultStatusNotFound);
    }
}
