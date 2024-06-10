namespace MindSweeper.Cli.UnitTests.CommandTests.Status;

/// <summary>
/// Unit tests for the StatusCommand class.
/// </summary>
public class StatusCommandTests
{
    private readonly CliRootCommand _rootCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCommandTests"/> class.
    /// </summary>
    public StatusCommandTests()
    {
        _rootCommand = new RootCommand();
    }

    /// <summary>
    /// Tests the StatusCommand with no options or arguments, and verifies that it produces no errors.
    /// </summary>
    [Fact]
    public void StatusCommand_With_No_Options_Or_args_Produces_No_Errors()
    {
        // Act
        var result = _rootCommand.Parse("testhost status");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("status");
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }
}
