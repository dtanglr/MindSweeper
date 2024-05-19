namespace SchneiderElectric.MindSweeper.Cli;

internal static class Extensions
{
    internal static IEnumerable<T> RecurseWhileNotNull<T>(this T? source, Func<T, T?> next) where T : class
    {
        while (source is not null)
        {
            yield return source;

            source = next(source);
        }
    }
}
