namespace MindSweeper.Cli;

/// <summary>
/// Represents a game console interface.
/// </summary>
internal interface IGameConsole : IConsole
{
    /// <summary>
    /// Writes a string value to the console.
    /// </summary>
    /// <param name="value">The string value to write.</param>
    void Write(string value) => Out.Write(value);

    /// <summary>
    /// Writes a formatted string to the console.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    void Write(string format, params object?[] args)
    {
        if (args.Length == 0)
        {
            Out.Write(format);
            return;
        }

        var formatted = string.Format(format, args);
        Out.Write(formatted);
    }

    /// <summary>
    /// Writes a new line to the console.
    /// </summary>
    void WriteLine() => Out.WriteLine();

    /// <summary>
    /// Writes a string value followed by a new line to the console.
    /// </summary>
    /// <param name="value">The string value to write.</param>
    void WriteLine(string value) => Out.WriteLine(value);

    /// <summary>
    /// Writes a formatted string followed by a new line to the console.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    void WriteLine(string format, params object?[] args)
    {
        if (args.Length == 0)
        {
            Out.WriteLine(format);
            return;
        }

        var formatted = string.Format(format, args);
        Out.WriteLine(formatted);
    }

    /// <summary>
    /// Writes the game status to the console.
    /// </summary>
    /// <param name="game">The game object.</param>
    void Write(Game game)
    {
        switch (game.Status)
        {
            case GameStatus.InProgress:
                WriteLine(Resources.GameStatusRows, game.Settings.Rows);
                WriteLine(Resources.GameStatusColumns, game.Settings.Columns);
                WriteLine(Resources.GameStatusSquares, game.Settings.Squares);
                WriteLine(Resources.GameStatusBombs, game.Settings.Bombs);
                WriteLine(Resources.GameStatusCurrentSquare, game.CurrentSquare);
                WriteLine(Resources.GameStatusAvailableMoves, string.Join(", ", game.AvailableMoves.Select(m => $"{Environment.NewLine}    {m.Key} to {m.Value}")));
                WriteLine(Resources.GameStatusMoves, game.Moves);
                WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Won:
                WriteLine(Resources.GameStatusMoves, game.Moves);
                WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Lost:
                WriteLine(Resources.GameStatusMoves, game.Moves);
                WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                break;
            default:
                break;
        }
    }
}
