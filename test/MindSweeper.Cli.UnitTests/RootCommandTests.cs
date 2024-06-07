namespace MindSweeper.Cli.UnitTests;

public class RootCommandTests
{
    private readonly CliRootCommand _rootCommand;

    public RootCommandTests()
    {
        _rootCommand = Program.RootCommand;
    }

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
