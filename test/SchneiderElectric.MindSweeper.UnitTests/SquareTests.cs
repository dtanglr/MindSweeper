namespace SchneiderElectric.MindSweeper.UnitTests;

public class SquareTests
{
    [Fact]
    public void GivenDefaultSettings_AboveTheMinimumRequired_SquaresInstantiateCorrectly()
    {
        // Arrange
        var settings = new Settings();

        // Act
        var sut = new Field.Squares(settings);

        // Assert
        sut.Length.Should().Be(settings.Squares);

        for (var i = 0; i < settings.Squares; i++)
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
        var settings = new Settings();
        var squares = new Field.Squares(settings);

        // Act
        var square = squares.GetStartSquare();

        // Assert
        square.Should().NotBeNull();
        square.Row.Index.Should().Be(0);
        square.Row.Should().BeOfType<FirstRow>();
        square.Index.Should().BeLessThan(settings.Columns);
    }

    [Fact]
    public void GivenACollectionOfBombs_SquareHasBombMethod_CorrectlyReturnsWhetherTheSquareHasABomb()
    {
        // Arrange
        var settings = new Settings();
        var bombs = new Field.Bombs(settings);
        var listOfBombs = bombs.ToList();
        var squares = new Field.Squares(settings);

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
