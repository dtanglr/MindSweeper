using MindSweeper.Domain.Components;

namespace MindSweeper.Domain.UnitTests.ComponentTests;

public class BombTests
{
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
