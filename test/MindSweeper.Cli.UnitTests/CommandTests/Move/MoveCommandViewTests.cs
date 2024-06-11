using MindSweeper.Application.Mediator.Commands.Move;
using MindSweeper.Cli.Commands.Move;

namespace MindSweeper.Cli.UnitTests.CommandTests.Move;

/// <summary>
/// Unit tests for the MoveCommandView class.
/// </summary>
public class MoveCommandViewTests
{
    private readonly IGameConsole _console;
    private readonly Fixture _fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandViewTests"/> class.
    /// </summary>
    public MoveCommandViewTests()
    {
        _console = new GameTestConsole();
        _fixture = new Fixture();
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Accepted result with GameStatus.InProgress.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_InProgress_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .Create();
        var response = new MoveCommandResponse(game);
        var result = Result<MoveCommandResponse>.Accepted(response);

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.MoveCommandResultStatusAccepted);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Accepted result with GameStatus.Won.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_Won_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.Won)
            .Create();
        var response = new MoveCommandResponse(game);
        var result = Result<MoveCommandResponse>.Accepted(response);

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.GameOver);
        output.Should().Contain(Resources.YouWin);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Accepted result with GameStatus.Lost.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_Lost_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.Lost)
            .Create();
        var response = new MoveCommandResponse(game);
        var result = Result<MoveCommandResponse>.Accepted(response);

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.Boom);
        output.Should().Contain(Resources.GameOver);
        output.Should().Contain(Resources.YouLose);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Accepted result with a hit bomb.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_HitBomb_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var move = _fixture.Build<Domain.Move>()
            .With(m => m.HitBomb, true)
            .Create();
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .With(g => g.LastMove, move)
            .Create();
        var response = new MoveCommandResponse(game);
        var result = Result<MoveCommandResponse>.Accepted(response);

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.Boom);
        output.Should().Contain(Resources.MoveCommandDidHitBomb);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Accepted result without hitting a bomb.
    /// </summary>
    [Fact]
    public void Render_Accepted_Result_Should_Display_Accepted_DidNotHitBomb_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var move = _fixture.Build<Domain.Move>()
            .With(m => m.HitBomb, false)
            .Create();
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .With(g => g.LastMove, move)
            .Create();
        var response = new MoveCommandResponse(game);
        var result = Result<MoveCommandResponse>.Accepted(response);

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.Yes);
        output.Should().Contain(Resources.MoveCommandDidNotHitBomb);
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for an Forbidden result.
    /// </summary>
    [Fact]
    public void Render_Forbidden_Result_Should_Display_Forbidden_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var result = Result<MoveCommandResponse>.Forbidden();

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.MoveCommandResultStatusForbidden, request.Direction));
    }

    /// <summary>
    /// Test case to verify that the Render method displays the correct message for a NotFound result.
    /// </summary>
    [Fact]
    public void Render_NotFound_Result_Should_Display_NotFound_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var result = Result<MoveCommandResponse>.NotFound();

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.MoveCommandResultStatusNotFound);
    }
}
