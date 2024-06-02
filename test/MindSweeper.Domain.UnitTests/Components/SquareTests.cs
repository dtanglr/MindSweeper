using MindSweeper.Domain.Components;
using MindSweeper.Domain.Components.Columns;
using MindSweeper.Domain.Components.Rows;

namespace MindSweeper.Domain.UnitTests.Components;

public class SquareTests
{
    [Fact]
    public void GivenDefaultSettings_AboveTheMinimumRequired_SquaresInstantiateCorrectly()
    {
        // Arrange
        var settings = new GameSettings();

        // Act
        var sut = new Field.Squares(settings);

        // Assert
        sut.Length.Should().Be(settings.Squares);

        for (var i = 0; i < settings.Squares; i++)
        {
            var @this = sut[i];
            @this.Index.Should().Be(i);
            @this.Name.Should().Be($"{@this.Column.Name}{@this.Row.Name}");
            @this.IsOnLastRow.Should().Be(@this.Row is not IHasRowAbove);

            var that = sut[@this.Name];
            @this.Should().Be(that);
        }
    }

    [Fact]
    public void GivenDefaultSettings_TryMoveMethod_EachSquareReturnsCorrectly()
    {
        // Arrange
        var settings = new GameSettings();
        var directions = Enum.GetValues<Direction>();

        // Act
        var sut = new Field.Squares(settings);

        // Assert
        for (var i = 0; i < settings.Squares; i++)
        {
            var fromSquare = sut[i];

            for (var j = 0; j < directions.Length; j++)
            {
                var direction = directions[j];
                var canMove = fromSquare.TryMove(direction, out var toSquare);

                switch (direction)
                {
                    case Direction.Up:

                        if (fromSquare.Row is not IHasRowAbove)
                        {
                            canMove.Should().BeFalse();
                            toSquare.Should().BeNull();
                        }
                        else
                        {
                            canMove.Should().BeTrue();
                            toSquare.Should().NotBeNull();
                            toSquare!.Row.Index.Should().Be(fromSquare.Row.Index + 1);
                            toSquare!.Column.Index.Should().Be(fromSquare.Column.Index);
                        }

                        break;

                    case Direction.Down:

                        if (fromSquare.Row is not IHasRowBelow)
                        {
                            canMove.Should().BeFalse();
                            toSquare.Should().BeNull();
                        }
                        else
                        {
                            canMove.Should().BeTrue();
                            toSquare.Should().NotBeNull();
                            toSquare!.Row.Index.Should().Be(fromSquare.Row.Index - 1);
                            toSquare!.Column.Index.Should().Be(fromSquare.Column.Index);
                        }

                        break;

                    case Direction.Left:

                        if (fromSquare.Column is not IHasColumnOnLeft)
                        {
                            canMove.Should().BeFalse();
                            toSquare.Should().BeNull();
                        }
                        else
                        {
                            canMove.Should().BeTrue();
                            toSquare.Should().NotBeNull();
                            toSquare!.Row.Index.Should().Be(fromSquare.Row.Index);
                            toSquare!.Column.Index.Should().Be(fromSquare.Column.Index - 1);
                        }

                        break;

                    case Direction.Right:

                        if (fromSquare.Column is not IHasColumnOnRight)
                        {
                            canMove.Should().BeFalse();
                            toSquare.Should().BeNull();
                        }
                        else
                        {
                            canMove.Should().BeTrue();
                            toSquare.Should().NotBeNull();
                            toSquare!.Row.Index.Should().Be(fromSquare.Row.Index);
                            toSquare!.Column.Index.Should().Be(fromSquare.Column.Index + 1);
                        }

                        break;
                }
            }
        }
    }

    [Fact]
    public void GivenACollectionOfSquares_SquareGetStartSquareMethod_CorrectlyReturnsASquareOnTheFirstRow()
    {
        // Arrange
        var settings = new GameSettings();
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
        var settings = new GameSettings();
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
