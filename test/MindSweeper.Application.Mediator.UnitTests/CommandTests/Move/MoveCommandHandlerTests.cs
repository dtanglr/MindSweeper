using MindSweeper.Application.Mediator.Commands.Move;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.Move;

/// <summary>
/// Unit tests for the <see cref="MoveCommandHandler"/> class.
/// </summary>
public class MoveCommandHandlerTests
{
    /// <summary>
    /// Tests the <see cref="MoveCommandHandler.Handle"/> method moves the player from their current square in the given direction and returns the expected result.
    /// </summary>
    /// <param name="status">The result status.</param>
    [Theory]
    [InlineData(ResultStatus.Accepted)]
    [InlineData(ResultStatus.NotFound)]
    [InlineData(ResultStatus.Forbidden)]
    [InlineData(ResultStatus.Unprocessable)]
    [InlineData(ResultStatus.Error)]
    public async Task Handle_WhenServiceMovestheGame_ReturnsExpectedResult(ResultStatus status)
    {
        // Arrange
        var fixture = new Fixture();
        var service = Substitute.For<IGameService>();
        var handler = new MoveCommandHandler(service);
        var direction = Direction.Up;
        var request = new MoveCommandRequest(direction);
        var cancellationToken = CancellationToken.None;
        var game = fixture.Create<Game>();
        var result = status switch
        {
            ResultStatus.Accepted => Result<Game>.Accepted(game),
            ResultStatus.NotFound => Result<Game>.NotFound(),
            ResultStatus.Forbidden => Result<Game>.Forbidden(),
            ResultStatus.Unprocessable => Result<Game>.Unprocessable("unprocessable"),
            ResultStatus.Error => Result<Game>.Error("error"),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        service.MoveAsync(Arg.Any<Direction>(), Arg.Any<CancellationToken>()).Returns(result);

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        await service.Received(1).MoveAsync(direction, cancellationToken);
        response.Should().BeEquivalentTo(result.ToResult(response.Value));
    }
}
