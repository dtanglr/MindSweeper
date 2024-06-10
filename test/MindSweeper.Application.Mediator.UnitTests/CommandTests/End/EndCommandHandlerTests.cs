using MindSweeper.Application.Mediator.Commands.End;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.End;

/// <summary>
/// Unit tests for the <see cref="EndCommandHandler"/> class.
/// </summary>
public class EndCommandHandlerTests
{
    /// <summary>
    /// Tests the <see cref="EndCommandHandler.Handle"/> method when the service ends the game and returns the expected result.
    /// </summary>
    /// <param name="status">The result status.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the status is not a valid <see cref="ResultStatus"/>.</exception>
    [Theory]
    [InlineData(ResultStatus.Accepted)]
    [InlineData(ResultStatus.NotFound)]
    [InlineData(ResultStatus.Unprocessable)]
    [InlineData(ResultStatus.Error)]
    public async Task Handle_WhenServiceEndsGame_ReturnsExpectedResult(ResultStatus status)
    {
        // Arrange
        var fixture = new Fixture();
        var service = Substitute.For<IGameService>();
        var handler = new EndCommandHandler(service);
        var request = new EndCommandRequest();
        var cancellationToken = CancellationToken.None;
        var result = status switch
        {
            ResultStatus.Accepted => Result.Accepted(),
            ResultStatus.NotFound => Result.NotFound(),
            ResultStatus.Unprocessable => Result.Unprocessable(),
            ResultStatus.Error => Result.Error(),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        service.EndAsync(Arg.Any<CancellationToken>()).Returns(result);

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        await service.Received(1).EndAsync(cancellationToken);
        response.Should().Be(result);
    }
}
