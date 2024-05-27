using System.Text;

namespace MindSweeper.Cli;

internal static class Extensions
{
    /// <summary>
    /// Recursively iterates over the source object while it is not null.
    /// </summary>
    /// <typeparam name="T">The type of the source object.</typeparam>
    /// <param name="source">The source object.</param>
    /// <param name="next">The function to get the next object in the iteration.</param>
    /// <returns>An enumerable of the source object and its descendants.</returns>
    public static IEnumerable<T> RecurseWhileNotNull<T>(this T? source, Func<T, T?> next) where T : class
    {
        while (source is not null)
        {
            yield return source;

            source = next(source);
        }
    }

    /// <summary>
    /// Gets the game status as a formatted string.
    /// </summary>
    /// <param name="game">The game object.</param>
    /// <returns>A StringBuilder containing the formatted game status.</returns>
    public static StringBuilder GetGameStatus(this Game game)
    {
        var sb = new StringBuilder();

        switch (game.Status)
        {
            case GameStatus.InProgress:
                sb.AppendLine(string.Format(Resources.GameStatusRows, game.Settings.Rows));
                sb.AppendLine(string.Format(Resources.GameStatusColumns, game.Settings.Columns));
                sb.AppendLine(string.Format(Resources.GameStatusSquares, game.Settings.Squares));
                sb.AppendLine(string.Format(Resources.GameStatusBombs, game.Settings.Bombs));
                sb.AppendLine(string.Format(Resources.GameStatusCurrentSquare, game.CurrentSquare));
                sb.AppendLine(string.Format(Resources.GameStatusAvailableMoves, string.Join(", ", game.AvailableMoves.Select(m => $"{Environment.NewLine}    {m.Key} to {m.Value}"))));
                sb.AppendLine(string.Format(Resources.GameStatusMoves, game.Moves));
                sb.AppendLine(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
                sb.AppendLine(string.Format(Resources.GameStatusLives, game.Lives));
                break;
            case GameStatus.Won:
                sb.AppendLine(string.Format(Resources.GameStatusMoves, game.Moves));
                sb.AppendLine(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
                sb.AppendLine(string.Format(Resources.GameStatusLives, game.Lives));
                break;
            case GameStatus.Lost:
                sb.AppendLine(string.Format(Resources.GameStatusMoves, game.Moves));
                sb.AppendLine(string.Format(Resources.GameStatusBombsHit, game.BombsHit));
                break;
            default:
                break;
        }

        return sb;
    }
}
