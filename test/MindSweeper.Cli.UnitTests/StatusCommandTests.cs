namespace MindSweeper.Cli.UnitTests;

public class StatusCommandTests
{
    private readonly CliRootCommand _rootCommand;

    public StatusCommandTests()
    {
        _rootCommand = Program.RootCommand;
    }

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
