using MindSweeper.Domain.Components;
using MindSweeper.Domain.Components.Columns;

namespace MindSweeper.Domain.UnitTests.Components;

public class ColumnTests
{
    [Fact]
    public void GivenACapacity_AboveTheMinimumRequired_ColumnsInstantiateCorrectly()
    {
        // Arrange
        const int Capacity = 8;

        // Act
        var sut = new Field.Columns(Capacity);

        // Assert
        sut.Length.Should().Be(Capacity);

        for (var i = 0; i < Capacity; i++)
        {
            var column = sut[i];
            column.Index.Should().Be(i);
            column.Name.Should().Be(GameSettings.ColumnNames[i]);
            column.Columns.Should().Be(sut);

            if (i == 0)
            {
                column.Should().BeOfType<FirstColumn>();
                column.Should().NotBeAssignableTo<IHasColumnOnLeft>();
                column.Should().BeAssignableTo<IHasColumnOnRight>();
                ((IHasColumnOnRight)column).Right.Should().Be(sut[i + 1]);
            }
            else if (i == Capacity - 1)
            {
                column.Should().BeOfType<LastColumn>();
                column.Should().BeAssignableTo<IHasColumnOnLeft>();
                column.Should().NotBeAssignableTo<IHasColumnOnRight>();
                ((IHasColumnOnLeft)column).Left.Should().Be(sut[i - 1]);
            }
            else
            {
                column.Should().BeOfType<MiddleColumn>();
                column.Should().BeAssignableTo<IHasColumnOnLeft>();
                column.Should().BeAssignableTo<IHasColumnOnRight>();
                ((IHasColumnOnLeft)column).Left.Should().Be(sut[i - 1]);
                ((IHasColumnOnRight)column).Right.Should().Be(sut[i + 1]);
            }
        }
    }
}
