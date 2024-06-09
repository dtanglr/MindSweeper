using System.CommandLine.Parsing;

namespace MindSweeper.Cli;

/// <summary>
/// Provides extension methods for the IConsole interface.
/// </summary>
internal static class Extensions
{
    /// <summary>
    /// Writes a string value to the console.
    /// </summary>
    /// <param name="console">The console object.</param>
    /// <param name="value">The string value to write.</param>
    public static void Write(this IConsole console, string value) => console.Out.Write(value);

    /// <summary>
    /// Writes a formatted string to the console.
    /// </summary>
    /// <param name="console">The console object.</param>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format the string.</param>
    public static void Write(this IConsole console, string format, params object?[] args)
    {
        if (args.Length == 0)
        {
            console.Out.Write(format);
            return;
        }

        var formatted = string.Format(format, args);
        console.Out.Write(formatted);
    }

    /// <summary>
    /// Writes a new line to the console.
    /// </summary>
    /// <param name="console">The console object.</param>
    public static void WriteLine(this IConsole console) => console.Out.WriteLine();

    /// <summary>
    /// Writes a string value followed by a new line to the console.
    /// </summary>
    /// <param name="console">The console object.</param>
    /// <param name="value">The string value to write.</param>
    public static void WriteLine(this IConsole console, string value) => console.Out.WriteLine(value);

    /// <summary>
    /// Writes a formatted string followed by a new line to the console.
    /// </summary>
    /// <param name="console">The console object.</param>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format the string.</param>
    public static void WriteLine(this IConsole console, string format, params object?[] args)
    {
        if (args.Length == 0)
        {
            console.Out.WriteLine(format);
            return;
        }

        var formatted = string.Format(format, args);
        console.Out.WriteLine(formatted);
    }

    /// <summary>
    /// Writes the game status to the console based on the current game state.
    /// </summary>
    /// <param name="console">The console object.</param>
    /// <param name="game">The game object.</param>
    public static void Write(this IConsole console, Game game)
    {
        switch (game.Status)
        {
            case GameStatus.InProgress:
                console.WriteLine(Resources.GameStatusRows, game.Settings.Rows);
                console.WriteLine(Resources.GameStatusColumns, game.Settings.Columns);
                console.WriteLine(Resources.GameStatusSquares, game.Settings.Squares);
                console.WriteLine(Resources.GameStatusBombs, game.Settings.Bombs);
                console.WriteLine(Resources.GameStatusCurrentSquare, game.CurrentSquare);
                console.WriteLine(Resources.GameStatusAvailableMoves, string.Join(", ", game.AvailableMoves.Select(m => $"{Environment.NewLine}    {m.Key} to {m.Value}")));
                console.WriteLine(Resources.GameStatusMoves, game.Moves);
                console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                console.WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Won:
                console.WriteLine(Resources.GameStatusMoves, game.Moves);
                console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                console.WriteLine(Resources.GameStatusLives, game.Lives);
                break;
            case GameStatus.Lost:
                console.WriteLine(Resources.GameStatusMoves, game.Moves);
                console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Recursively iterates over the source object while it is not null.
    /// </summary>
    /// <typeparam name="T">The type of the source object.</typeparam>
    /// <param name="source">The source object.</param>
    /// <param name="next">The function to get the next object in the iteration.</param>
    /// <returns>An enumerable of the source object and its descendants.</returns>
    public static IEnumerable<T> RecurseWhileNotNull<T>(this T? source, Func<T, T?> next) where T : SymbolResult
    {
        while (source is not null)
        {
            yield return source;

            source = next(source);
        }
    }

    /// <summary>
    /// Adds an error message to the symbol result.
    /// </summary>
    /// <param name="result">The symbol result.</param>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format the string.</param>
    public static void AddError(this SymbolResult result, string format, params object?[] args) =>
        result.AddError(string.Format(format, args));
}
