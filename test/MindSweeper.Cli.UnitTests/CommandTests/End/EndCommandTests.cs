namespace MindSweeper.Cli.UnitTests.CommandTests.End;

/// <summary>
/// Unit tests for the EndCommand class.
/// </summary>
public class EndCommandTests
{
    private readonly CliRootCommand _rootCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandTests"/> class.
    /// </summary>
    public EndCommandTests()
    {
        _rootCommand = new RootCommand();
    }

    /// <summary>
    /// Tests the EndCommand with no options or arguments, and verifies that it produces no errors.
    /// </summary>
    [Fact]
    public void EndCommand_With_No_Options_Or_args_Produces_No_Errors()
    {
        // Act
        var result = _rootCommand.Parse("testhost end");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("end");
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }
}
