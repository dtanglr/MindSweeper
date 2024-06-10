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
}
