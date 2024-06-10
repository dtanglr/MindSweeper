namespace MindSweeper.Cli.UnitTests.CommandTests.Move;

/// <summary>
/// Unit tests for the MoveCommand class.
/// </summary>
public class MoveCommandTests
{
    private readonly CliRootCommand _rootCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandTests"/> class.
    /// </summary>
    public MoveCommandTests()
    {
        _rootCommand = new RootCommand();
    }

    /// <summary>
    /// Tests the MoveCommand without the required direction argument and verifies that it produces an error.
    /// </summary>
    [Fact]
    public void MoveCommand_Without_Required_Direction_Argument_Produces_Error()
    {
        // Act
        var result = _rootCommand.Parse("testhost move");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("move");
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be("Required argument missing for command: 'move'.");
        result.Errors[0].SymbolResult.As<ArgumentResult>().Argument.Name.Should().Be("direction");
        result.UnmatchedTokens.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the MoveCommand with the specified direction argument and verifies that it produces no errors.
    /// </summary>
    /// <param name="direction">The direction argument.</param>
    [Theory]
    [InlineData(Direction.Up)]
    [InlineData(Direction.Down)]
    [InlineData(Direction.Left)]
    [InlineData(Direction.Right)]
    public void MoveCommand_With_Direction_Argument_Produces_No_Errors(Direction direction)
    {
        // Act
        var result = _rootCommand.Parse($"testhost move {direction}");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("move");
        result.CommandResult.Tokens.Should().HaveCount(1);
        result.CommandResult.Tokens[0].Value.Should().Be(direction.ToString());
        result.Errors.Should().BeEmpty();
        result.UnmatchedTokens.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the MoveCommand with multiple direction arguments and verifies that it produces an error.
    /// </summary>
    [Fact]
    public void MoveCommand_With_Mulitple_Direction_Arguments_Produces_Error()
    {
        // Act
        var result = _rootCommand.Parse($"testhost move up up");

        // Assert
        result.RootCommandResult.Command.Name.Should().Be("testhost");
        result.CommandResult.Command.Name.Should().Be("move");
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be("Unrecognized command or argument 'up'.");
        result.UnmatchedTokens.Should().NotBeEmpty();
        result.UnmatchedTokens[0].Should().Be("up");
    }
}
