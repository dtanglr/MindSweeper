using MindSweeper.Domain.Components;

namespace MindSweeper.Domain.UnitTests.ComponentTests;

/// <summary>
/// Unit tests for the Bomb class.
/// </summary>
public class BombTests
{
    /// <summary>
    /// Tests that bombs instantiate correctly with default settings.
    /// </summary>
    [Fact]
    public void GivenDefaultSettings_BombsInstantiateCorrectly()
    {
        // Arrange
        var settings = new GameSettings();

        // Act
        var sut = new Field.Bombs(settings);

        // Assert
        sut.Length.Should().Be(settings.Bombs);
        sut.ToList().ForEach(bomb =>
        {
            bomb.Should().BeGreaterThan(-1);
            bomb.Should().BeLessThan(settings.Squares);
        });
    }

    /// <summary>
    /// Tests that bombs instantiate correctly with a list of bombs.
    /// </summary>
    [Fact]
    public void GivenAListOfBombs_BombsInstantiateCorrectly()
    {
        // Arrange
        var settings = new GameSettings();
        var bombs = new Field.Bombs(settings);
        var listOfBombs = bombs.ToList();

        // Act
        var sut = new Field.Bombs(listOfBombs);

        // Assert
        sut.ToList().Should().BeEquivalentTo(listOfBombs);
    }

    /// <summary>
    /// Tests that the Bombs.OnSquare method correctly returns squares with bombs.
    /// </summary>
    [Fact]
    public void GivenACollectionOfSquares_BombsOnSquareMethod_CorrectlyReturnsSquaresWithBombs()
    {
        // Arrange
        var settings = new GameSettings();
        var squares = new Field.Squares(settings);

        // Act
        var bombs = new Field.Bombs(settings);
        var listOfBombs = bombs.ToList();

        // Assert
        for (var i = 0; i < squares.Length; i++)
        {
            var square = squares[i];
            var isBombOnSquare = bombs.OnSquare(square);
            var isBombOnSquareExpected = listOfBombs.Contains(square.Index);
            isBombOnSquare.Should().Be(isBombOnSquareExpected);
        }
    }
}
