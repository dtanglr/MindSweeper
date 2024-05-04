namespace SchneiderElectric.MindSweeper.UnitTests;

public class BombTests
{
    [Fact]
    public void GivenAColumnRowAndBombCapacity_AboveTheMinimumRequired_BombsInstantiateCorrectly()
    {
        // Arrange
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;
        const int BombsCapacity = 12;

        // Act
        var sut = new Field.Bombs(ColumnsCapacity, RowsCapacity, BombsCapacity);

        // Assert
        sut.Length.Should().Be(BombsCapacity);
        sut.ToList().ForEach(bomb =>
        {
            bomb.Should().BeGreaterThan(-1);
            bomb.Should().BeLessThan(ColumnsCapacity * RowsCapacity);
        });
    }

    [Fact]
    public void GivenAListOfBombs_BombsInstantiateCorrectly()
    {
        // Arrange
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;
        const int BombsCapacity = 12;
        var bombs = new Field.Bombs(ColumnsCapacity, RowsCapacity, BombsCapacity);
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
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;
        const int BombsCapacity = 12;
        var squares = new Field.Squares(ColumnsCapacity, RowsCapacity);

        // Act
        var bombs = new Field.Bombs(ColumnsCapacity, RowsCapacity, BombsCapacity);
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
