namespace MindSweeper.Cli.UnitTests.CommandTests;

public class EndCommandTests
{
    private readonly CliRootCommand _rootCommand;

    public EndCommandTests()
    {
        _rootCommand = new RootCommand();
    }

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
