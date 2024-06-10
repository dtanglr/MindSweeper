using MindSweeper.Cli.Commands;

namespace MindSweeper.Cli.UnitTests.CommandTests;

/// <summary>
/// Unit tests for the ErrorView class.
/// </summary>
public class ErrorViewTests
{
    private readonly IGameConsole _console;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorViewTests"/> class.
    /// </summary>
    public ErrorViewTests()
    {
        _console = new GameTestConsole();
    }

    /// <summary>
    /// Test case to verify that rendering an invalid result displays the invalid message.
    /// </summary>
    [Fact]
    public void Render_Invalid_Result_Should_Display_Invalid_Message()
    {
        // Arrange
        var view = new ErrorView(_console);
        var invalidMessage = "Invalid message";
        var result = Result.Invalid([new ValidationIssue("id", invalidMessage)]);

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.CommandResultStatusInvalid);
        output.Should().Contain(invalidMessage);
    }

    /// <summary>
    /// Test case to verify that rendering an error result displays the error message.
    /// </summary>
    [Fact]
    public void Render_Error_Result_Should_Display_Error_Message()
    {
        // Arrange
        var view = new ErrorView(_console);
        var errorMessage = "Error message";
        var result = Result.Error(errorMessage);

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(Resources.CommandResultStatusError);
        output.Should().Contain(errorMessage);
    }

    /// <summary>
    /// Test case to verify that rendering an unhandled result displays the unhandled message.
    /// </summary>
    [Fact]
    public void Render_Unhandled_Result_Should_Display_Unhandled_Message()
    {
        // Arrange
        var view = new ErrorView(_console);
        var result = Result.Forbidden();

        // Act
        view.Render(result);

        // Assert
        var output = _console.Out.ToString();
        output.Should().Contain(string.Format(Resources.CommandResultStatusUnhandled, ResultStatus.Forbidden));
    }
}
