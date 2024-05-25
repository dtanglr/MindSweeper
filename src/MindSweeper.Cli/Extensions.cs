using System.Text;

namespace MindSweeper.Cli;

internal static class Extensions
{
    public static IEnumerable<T> RecurseWhileNotNull<T>(this T? source, Func<T, T?> next) where T : class
    {
        while (source is not null)
        {
            yield return source;

            source = next(source);
        }
    }

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
