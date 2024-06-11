using MindSweeper.Application.Mediator.Commands.End;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.End;

/// <summary>
/// Unit tests for the <see cref="EndCommandHandler"/> class.
/// </summary>
public class EndCommandHandlerTests
{
    /// <summary>
    /// Tests the <see cref="EndCommandHandler.Handle"/> method ends the game and returns the expected result.
    /// </summary>
    /// <param name="status">The result status.</param>
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
            ResultStatus.Unprocessable => Result.Unprocessable("unprocessable"),
            ResultStatus.Error => Result.Error("error"),
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
