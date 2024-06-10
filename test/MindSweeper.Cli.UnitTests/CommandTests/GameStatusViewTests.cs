using MindSweeper.Cli.Commands;

namespace MindSweeper.Cli.UnitTests.CommandTests;

/// <summary>
/// Unit tests for the GameStatusView class.
/// </summary>
public class GameStatusViewTests
{
    private readonly IGameConsole _console;
    private readonly Fixture _fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStatusViewTests"/> class.
    /// </summary>
    public GameStatusViewTests()
    {
        _console = new GameTestConsole();
        _fixture = new Fixture();
    }

    /// <summary>
    /// Test case for rendering in-progress game status.
    /// </summary>
    [Fact]
    public void Render_InProgress_Status_Should_Display_InProgress_Messages()
    {
        // Arrange
        var view = new GameStatusView(_console);
        var settings = new GameSettings();
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.InProgress)
            .With(g => g.Settings, settings)
            .Create();

        // Act
        view.Render(game);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.GameStatusRows, game.Settings.Rows));
        output.Should().Contain(string.Format(Resources.GameStatusColumns, game.Settings.Columns));
        output.Should().Contain(string.Format(Resources.GameStatusSquares, game.Settings.Squares));
        output.Should().Contain(string.Format(Resources.GameStatusBombs, game.Settings.Bombs));
        output.Should().Contain(string.Format(Resources.GameStatusCurrentSquare, game.CurrentSquare));
        output.Should().Contain(string.Format(Resources.GameStatusMoves, game.Moves));
        output.Should().Contain(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
        output.Should().Contain(string.Format(Resources.GameStatusLives, game.Lives));
    }

    /// <summary>
    /// Test case for rendering won game status.
    /// </summary>
    [Fact]
    public void Render_Won_Status_Result_Should_Display_Won_Messages()
    {
        // Arrange
        var view = new GameStatusView(_console);
        var settings = new GameSettings();
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.Won)
            .With(g => g.Settings, settings)
            .Create();

        // Act
        view.Render(game);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.GameStatusMoves, game.Moves));
        output.Should().Contain(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
        output.Should().Contain(string.Format(Resources.GameStatusLives, game.Lives));
    }

    /// <summary>
    /// Test case for rendering lost game status.
    /// </summary>
    [Fact]
    public void Render_Lost_Status_Result_Should_Display_Lost_Messages()
    {
        // Arrange
        var view = new GameStatusView(_console);
        var settings = new GameSettings();
        var game = _fixture.Build<Game>()
            .With(g => g.Status, GameStatus.Won)
            .With(g => g.Settings, settings)
            .Create();

        // Act
        view.Render(game);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.GameStatusMoves, game.Moves));
        output.Should().Contain(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
    }
}
