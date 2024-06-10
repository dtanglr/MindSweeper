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
    void Write(string value);

    /// <summary>
    /// Writes a formatted string to the console using the specified format string and arguments.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format.</param>
    void Write(string format, params object?[] args);

    /// <summary>
    /// Writes a new line to the console.
    /// </summary>
    void WriteLine();

    /// <summary>
    /// Writes a string value followed by a new line to the console.
    /// </summary>
    /// <param name="value">The string value to write.</param>
    void WriteLine(string value);

    /// <summary>
    /// Writes a formatted string followed by a new line to the console using the specified format string and arguments.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format.</param>
    void WriteLine(string format, params object?[] args);
}
