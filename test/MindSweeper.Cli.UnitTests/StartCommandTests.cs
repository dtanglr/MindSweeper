namespace MindSweeper.Cli.UnitTests;

public class StartCommandTests
{
    private readonly CliRootCommand _rootCommand;

    public StartCommandTests()
    {
        _rootCommand = Program.RootCommand;
    }

    [Theory]
    [InlineData("testhost start")]
    [InlineData("testhost start --columns 8 --rows 8 --bombs 21 --lives 3")]
    [InlineData("testhost start -c 8 -r 8 -b 21 -l 3")]
    public void StartCommand_With_Valid_Options_Produces_Error(string commandLine)
    {
        // Act
        var result = _rootCommand.Parse(commandLine);

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("start");
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }

    [Theory]
    [InlineData("testhost start --columns 8 --columns 8", "--columns")]
    [InlineData("testhost start --columns 8 -c 8", "--columns")]
    [InlineData("testhost start -c 8 -c 8", "-c")]
    [InlineData("testhost start -c 8 --columns 8", "-c")]
    [InlineData("testhost start --rows 8 --rows 8", "--rows")]
    [InlineData("testhost start --rows 8 -r 8", "--rows")]
    [InlineData("testhost start -r 8 -r 8", "-r")]
    [InlineData("testhost start -r 8 --rows 8", "-r")]
    [InlineData("testhost start --bombs 8 --bombs 8", "--bombs")]
    [InlineData("testhost start --bombs 8 -b 8", "--bombs")]
    [InlineData("testhost start -b 8 -b 8", "-b")]
    [InlineData("testhost start -b 8 --bombs 8", "-b")]
    [InlineData("testhost start --lives 8 --lives 8", "--lives")]
    [InlineData("testhost start --lives 8 -l 8", "--lives")]
    [InlineData("testhost start -l 8 -l 8", "-l")]
    [InlineData("testhost start -l 8 --lives 8", "-l")]
    public void StartCommand_With_Multiple_Options_Produces_Error(string commandLine, string expectedArgumentName)
    {
        // Act
        var result = _rootCommand.Parse(commandLine);

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("start");
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be($"Option '{expectedArgumentName}' expects a single argument but 2 were provided.");
        result.UnmatchedTokens.Should().BeEmpty();
    }

    //[Theory]
    //[InlineData($"--columns", GameSettings.DefaultColumns, GameSettings.MinimumColumns, GameSettings.MaximumColumns, true)]
    //[InlineData("-c", GameSettings.DefaultColumns, GameSettings.MinimumColumns, GameSettings.MaximumColumns, true)]
    //[InlineData("--rows", GameSettings.DefaultRows, GameSettings.MinimumRows, GameSettings.MaximumRows, true)]
    //[InlineData("-r", GameSettings.DefaultRows, GameSettings.MinimumRows, GameSettings.MaximumRows, true)]
    //[InlineData("--bombs", GameSettings.MinimumBombs, GameSettings.MinimumBombs, null, true)]
    //[InlineData("-b", GameSettings.MinimumBombs, GameSettings.MinimumBombs, null, true)]
    //[InlineData("--lives", GameSettings.DefaultLives, GameSettings.MinimumLives, null, true)]
}
