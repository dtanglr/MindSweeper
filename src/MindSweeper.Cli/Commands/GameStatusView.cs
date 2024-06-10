namespace MindSweeper.Cli.Commands;

/// <summary>
/// Represents a view for displaying the game status.
/// </summary>
internal class GameStatusView : ICommandView<Game>
{
    private readonly IGameConsole _console;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStatusView"/> class.
    /// </summary>
    /// <param name="console">The game console used for output.</param>
    public GameStatusView(IGameConsole console)
    {
        _console = console;
    }

    /// <summary>
    /// Renders the game status based on the current game state.
    /// </summary>
    /// <param name="game">The game object containing the game state.</param>
    public void Render(Game game)
    {
        switch (game.Status)
        {
            case GameStatus.InProgress:
                _console.WriteLine(Resources.GameStatusRows, game.Settings.Rows);
                _console.WriteLine(Resources.GameStatusColumns, game.Settings.Columns);
                _console.WriteLine(Resources.GameStatusSquares, game.Settings.Squares);
                _console.WriteLine(Resources.GameStatusBombs, game.Settings.Bombs);
                _console.WriteLine(Resources.GameStatusCurrentSquare, game.CurrentSquare);
                _console.WriteLine(Resources.GameStatusAvailableMoves, string.Join(", ", game.AvailableMoves.Select(m => $"{Environment.NewLine}    {m.Key} to {m.Value}")));
                _console.WriteLine(Resources.GameStatusMoves, game.Moves);
                _console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                _console.WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Won:
                _console.WriteLine(Resources.GameStatusMoves, game.Moves);
                _console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                _console.WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Lost:
                _console.WriteLine(Resources.GameStatusMoves, game.Moves);
                _console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                break;
            default:
                break;
        }
    }
}
