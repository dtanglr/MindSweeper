namespace MindSweeper.Cli;

/// <summary>
/// Represents a test game console for the MindSweeper game.
/// </summary>
/// <remarks>
/// This class is a wrapper around the <see cref="TestConsole"/> class and implements the <see cref="IGameConsole"/> interface.
/// It facilitates the testing of the console output in unit tests.
/// </remarks>
internal class GameTestConsole : TestConsole, IGameConsole
{
}
