namespace SchneiderElectric.MindSweeper.UnitTests;

public class RowTests
{
    [Fact]
    public void GivenACapacity_AboveTheMinimumRequired_RowsInstantiateCorrectly()
    {
        // Arrange
        const int Capacity = 8;

        // Act
        var sut = new Field.Rows(Capacity);

        // Assert
        sut.Length.Should().Be(Capacity);

        for (var i = 0; i < Capacity; i++)
        {
            var row = sut[i];
            row.Index.Should().Be(i);
            row.Name.Should().Be($"{i + 1}");
            row.Rows.Should().Be(sut);

            if (i == 0)
            {
                row.Should().BeOfType<FirstRow>();
                row.Should().BeAssignableTo<IHasRowAbove>();
                row.Should().NotBeAssignableTo<IHasRowBelow>();
                ((IHasRowAbove)row).Above.Should().Be(sut[i + 1]);
            }
            else if (i == Capacity - 1)
            {
                row.Should().BeOfType<LastRow>();
                row.Should().NotBeAssignableTo<IHasRowAbove>();
                row.Should().BeAssignableTo<IHasRowBelow>();
                ((IHasRowBelow)row).Below.Should().Be(sut[i - 1]);
            }
            else
            {
                row.Should().BeOfType<MiddleRow>();
                row.Should().BeAssignableTo<IHasRowAbove>();
                row.Should().BeAssignableTo<IHasRowBelow>();
                ((IHasRowAbove)row).Above.Should().Be(sut[i + 1]);
                ((IHasRowBelow)row).Below.Should().Be(sut[i - 1]);
            }
        }
    }
}
