namespace MindSweeper.Cli;

/// <summary>
/// Represents the interface for the MindSweeper CLI program.
/// </summary>
public interface IProgram
{
    /// <summary>
    /// Gets the root command of the CLI program.
    /// </summary>
    abstract static CliRootCommand RootCommand { get; }

    /// <summary>
    /// Gets the start command of the CLI program.
    /// </summary>
    abstract static CliCommand StartCommand { get; }

    /// <summary>
    /// Gets the move command of the CLI program.
    /// </summary>
    abstract static CliCommand MoveCommand { get; }

    /// <summary>
    /// Gets the end command of the CLI program.
    /// </summary>
    abstract static CliCommand EndCommand { get; }

    /// <summary>
    /// Gets the status command of the CLI program.
    /// </summary>
    abstract static CliCommand StatusCommand { get; }

    /// <summary>
    /// The entry point of the CLI program.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>A task representing the program's exit code.</returns>
    abstract static Task<int> Main(string[] args);
}
