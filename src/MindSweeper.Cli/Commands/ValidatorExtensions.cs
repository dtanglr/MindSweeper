using System.CommandLine.Parsing;

namespace MindSweeper.Cli.Commands;

/// <summary>
/// Provides extension methods for validating symbol results.
/// </summary>
internal static class ValidatorExtensions
{
    /// <summary>
    /// Adds an error message to the symbol result.
    /// </summary>
    /// <param name="result">The symbol result.</param>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments to format the string.</param>
    public static void AddError(this SymbolResult result, string format, params object?[] args) =>
        result.AddError(string.Format(format, args));
}
