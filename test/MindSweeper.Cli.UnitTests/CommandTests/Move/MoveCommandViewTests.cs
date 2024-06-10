using MindSweeper.Application.Mediator.Commands.Move;
using MindSweeper.Cli.Commands.Move;
using MindSweeper.Cli.Properties;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.UnitTests.CommandTests.Move;

public class MoveCommandViewTests
{
    private readonly IGameConsole _console;
    private readonly IFixture _fixture;

    public MoveCommandViewTests()
    {
        _console = new GameTestConsole();
        _fixture = new Fixture();
    }

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

    [Fact]
    public void Render_Unprocessable_Result_Should_Display_Unprocessable_Message()
    {
        // Arrange
        var view = new MoveCommandView(_console);
        var request = new MoveCommandRequest(Direction.Up);
        var result = Result<MoveCommandResponse>.Unprocessable();

        // Act
        view.Render(request, result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.MoveCommandResultStatusUnprocessable, request.Direction));
    }

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
