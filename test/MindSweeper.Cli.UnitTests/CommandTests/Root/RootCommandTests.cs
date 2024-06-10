namespace MindSweeper.Cli.UnitTests.CommandTests.Root;

/// <summary>
/// Represents the unit tests for the RootCommand class.
/// </summary>
public class RootCommandTests
{
    private readonly CliRootCommand _rootCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="RootCommandTests"/> class.
    /// </summary>
    public RootCommandTests()
    {
        _rootCommand = new RootCommand();
    }

    /// <summary>
    /// Tests the RootCommand with no options or args and ensures no errors are produced.
    /// </summary>
    [Fact]
    public void RootCommand_With_No_Options_Or_args_Produces_No_Errors()
    {
        // Act
        var result = _rootCommand.Parse("testhost");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("testhost");
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }
}
