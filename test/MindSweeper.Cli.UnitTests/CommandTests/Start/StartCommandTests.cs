using MindSweeper.Cli.Properties;

namespace MindSweeper.Cli.UnitTests.CommandTests.Start;

/// <summary>
/// Represents the unit tests for the StartCommand class.
/// </summary>
public class StartCommandTests
{
    private readonly CliRootCommand _rootCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandTests"/> class.
    /// </summary>
    public StartCommandTests()
    {
        _rootCommand = new RootCommand();
    }

    /// <summary>
    /// Tests the StartCommand with valid options and ensures no errors are produced.
    /// </summary>
    /// <param name="commandLine">The command line to test.</param>
    [Theory]
    [InlineData("testhost start")]
    [InlineData("testhost start --columns 8 --rows 8 --bombs 21 --lives 3")]
    [InlineData("testhost start -c 8 -r 8 -b 21 -l 3")]
    public void StartCommand_With_Valid_Options_Produces_No_Errors(string commandLine)
    {
        // Act
        var result = _rootCommand.Parse(commandLine);

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("start");
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the StartCommand with invalid options above or below the threshold and ensures an error is produced.
    /// </summary>
    /// <param name="commandLine">The command line to test.</param>
    /// <param name="optionName">The name of the option.</param>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value to test.</param>
    [Theory]
    [InlineData("testhost start --columns 0", "--columns", GameSettings.MinimumColumns, 0)]
    [InlineData("testhost start -c 0", "-c", GameSettings.MinimumColumns, 0)]
    [InlineData("testhost start --columns 1000", "--columns", GameSettings.MaximumColumns, 1000)]
    [InlineData("testhost start -c 1000", "-c", GameSettings.MaximumColumns, 1000)]
    [InlineData("testhost start --rows 0", "--rows", GameSettings.MinimumRows, 0)]
    [InlineData("testhost start -r 0", "-r", GameSettings.MinimumRows, 0)]
    [InlineData("testhost start --rows 1000", "--rows", GameSettings.MaximumRows, 1000)]
    [InlineData("testhost start -r 1000", "-r", GameSettings.MaximumRows, 1000)]
    [InlineData("testhost start --bombs 0", "--bombs", GameSettings.MinimumBombs, 0)]
    [InlineData("testhost start -b 0", "-b", GameSettings.MinimumBombs, 0)]
    [InlineData("testhost start --lives 0", "--lives", GameSettings.MinimumLives, 0)]
    [InlineData("testhost start -l 0", "-l", GameSettings.MinimumLives, 0)]
    public void StartCommand_With_Invalid_Option_Above_Or_Below_Threshold_Produces_Error(string commandLine, string optionName, int threshold, int value)
    {
        // Assign
        var resource = value < threshold ? Resources.CommandOptionLessThanMinimum : Resources.CommandOptionGreaterThanMaximum;
        var errorMessage = string.Format(resource, optionName, threshold, value);

        // Act
        var result = _rootCommand.Parse(commandLine);

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("start");
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be(errorMessage);
        result.UnmatchedTokens.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the StartCommand with multiple options and ensures an error is produced.
    /// </summary>
    /// <param name="commandLine">The command line to test.</param>
    /// <param name="argumentName">The name of the argument.</param>
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
    public void StartCommand_With_Multiple_Options_Produces_Error(string commandLine, string argumentName)
    {
        // Act
        var result = _rootCommand.Parse(commandLine);

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("start");
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be($"Option '{argumentName}' expects a single argument but 2 were provided.");
        result.UnmatchedTokens.Should().BeEmpty();
    }
}
