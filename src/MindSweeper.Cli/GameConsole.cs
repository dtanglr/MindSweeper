namespace MindSweeper.Cli;

/// <summary>
/// Represents a game console for the MindSweeper game.
/// </summary>
/// <remarks>
/// This class is a wrapper around the <see cref="SystemConsole"/> class and implements the <see cref="IGameConsole"/> interface.
/// It provides additional methods to to extend <see cref="SystemConsole"/> so prevent the need to use the Out property for writing to the console.
/// </remarks>
internal class GameConsole : SystemConsole, IGameConsole
{
    /// <summary>
    /// Writes a string value to the console.
    /// </summary>
    /// <param name="value">The string value to write.</param>
    public void Write(string value) => Out.Write(value);

    /// <summary>
    /// Writes a formatted string to the console.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    public void Write(string format, params object?[] args)
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
    public void WriteLine() => Out.WriteLine();

    /// <summary>
    /// Writes a string value followed by a new line to the console.
    /// </summary>
    /// <param name="value">The string value to write.</param>
    public void WriteLine(string value) => Out.WriteLine(value);

    /// <summary>
    /// Writes a formatted string followed by a new line to the console.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    public void WriteLine(string format, params object?[] args)
    {
        if (args.Length == 0)
        {
            Out.WriteLine(format);
            return;
        }

        var formatted = string.Format(format, args);
        Out.WriteLine(formatted);
    }
}
