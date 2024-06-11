using MindSweeper.Application.Mediator.Commands.Start;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.Start;

/// <summary>
/// Unit tests for the StartCommandRequestValidator class.
/// </summary>
public class StartCommandRequestValidatorTests
{
    private readonly StartCommandRequestValidator _validator = new();

    [Fact]
    public void GivenDefaultGameSettings_WhenValidating_ThenShouldNotHaveAnyValidationErrors()
    {
        // Arrange
        var settings = new GameSettings();
        var startCommand = new StartCommandRequest(settings);

        // Act
        var result = _validator.Validate(startCommand);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenInvalidGameSettings_WhenValidating_ThenShouldHaveValidationErrors()
    {
        // Arrange
        var settings = new GameSettings { Rows = 0 };
        var startCommand = new StartCommandRequest(settings);

        // Act
        var result = _validator.Validate(startCommand);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
}
