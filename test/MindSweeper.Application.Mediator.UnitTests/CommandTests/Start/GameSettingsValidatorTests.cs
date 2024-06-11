using MindSweeper.Application.Mediator.Commands.Start;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.Start;

/// <summary>
/// Unit tests for the GameSettingsValidator class.
/// </summary>
public class GameSettingsValidatorTests
{
    private readonly GameSettingsValidator _validator = new();

    [Fact]
    public void GivenDefaultGameSettings_WhenValidating_ThenShouldNotHaveAnyValidationErrors()
    {
        // Arrange
        var settings = new GameSettings();

        // Act
        var result = _validator.Validate(settings);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(GameSettings.MinimumColumns - 1, GameSettings.MinimumRows, GameSettings.MinimumBombs, GameSettings.MinimumLives)]
    [InlineData(GameSettings.MaximumColumns + 1, GameSettings.MinimumRows, GameSettings.MinimumBombs, GameSettings.MinimumLives)]
    [InlineData(GameSettings.MinimumColumns, GameSettings.MinimumRows - 1, GameSettings.MinimumBombs, GameSettings.MinimumLives)]
    [InlineData(GameSettings.MinimumColumns, GameSettings.MaximumRows + 1, GameSettings.MinimumBombs, GameSettings.MinimumLives)]
    [InlineData(GameSettings.MinimumColumns, GameSettings.MinimumRows, GameSettings.MinimumBombs - 1, GameSettings.MinimumLives)]
    [InlineData(GameSettings.MinimumColumns, GameSettings.MinimumRows, GameSettings.MinimumBombs, GameSettings.MinimumLives - 1)]
    public void GivenGameSettings_WithInvalidRowSettings_WhenValidating_ThenShouldHaveValidationError(int columns, int rows, int bombs, int lives)
    {
        // Arrange
        var settings = new GameSettings { Columns = columns, Rows = rows, Bombs = bombs, Lives = lives };

        // Act
        var result = _validator.Validate(settings);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
}
