using SchneiderElectric.MindSweeper.Rows;

namespace SchneiderElectric.MindSweeper.UnitTests;

public class SquareTests
{
    [Fact]
    public void GivenAColumnAndRowCapacity_AboveTheMinimumRequired_SquaresInstantiateCorrectly()
    {
        // Arrange
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;

        // Act
        var sut = new Field.Squares(ColumnsCapacity, RowsCapacity);

        // Assert
        sut.Length.Should().Be(ColumnsCapacity * RowsCapacity);

        for (var i = 0; i < (ColumnsCapacity * RowsCapacity); i++)
        {
            var @this = sut[i];
            @this.Index.Should().Be(i);
            @this.Name.Should().Be($"{@this.Column.Name}{@this.Row.Name}");

            var that = sut[@this.Name];
            @this.Should().Be(that);
        }

        sut.GetStartSquare().Should().NotBeNull();
    }

    [Fact]
    public void GivenACollectionOfSquares_SquareGetStartSquareMethod_CorrectlyReturnsASquareOnTheFirstRow()
    {
        // Arrange
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;
        var squares = new Field.Squares(ColumnsCapacity, RowsCapacity);

        // Act
        var square = squares.GetStartSquare();

        // Assert
        square.Should().NotBeNull();
        square.Row.Index.Should().Be(0);
        square.Row.Should().BeOfType<FirstRow>();
        square.Index.Should().BeLessThan(ColumnsCapacity);
    }

    [Fact]
    public void GivenACollectionOfBombs_SquareHasBombMethod_CorrectlyReturnsWhetherTheSquareHasABomb()
    {
        // Arrange
        const int ColumnsCapacity = 8;
        const int RowsCapacity = 8;
        const int BombsCapacity = 12;
        var bombs = new Field.Bombs(ColumnsCapacity, RowsCapacity, BombsCapacity);
        var listOfBombs = bombs.ToList();
        var squares = new Field.Squares(ColumnsCapacity, RowsCapacity);

        for (var i = 0; i < squares.Length; i++)
        {
            var square = squares[i];
            var isBombOnSquareExpected = listOfBombs.Contains(square.Index);

            // Act
            var isBombOnSquare = square.HasBomb(bombs);

            // Assert
            isBombOnSquare.Should().Be(isBombOnSquareExpected);
        }
    }
}
