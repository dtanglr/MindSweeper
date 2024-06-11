using MindSweeper.Application.Mediator.Commands.Start;

namespace MindSweeper.Application.Mediator.UnitTests.CommandTests.Start;

/// <summary>
/// Unit tests for the <see cref="StartCommandHandler"/> class.
/// </summary>
public class StartCommandHandlerTests
{
    /// <summary>
    /// Tests the <see cref="StartCommandHandler.Handle"/> method starts the game and returns the expected result.
    /// </summary>
    /// <param name="status">The result status.</param>
    [Theory]
    [InlineData(ResultStatus.Accepted)]
    [InlineData(ResultStatus.Conflict)]
    [InlineData(ResultStatus.Unprocessable)]
    [InlineData(ResultStatus.Error)]
    public async Task Handle_WhenServiceStartstheGame_ReturnsExpectedResult(ResultStatus status)
    {
        // Arrange
        var fixture = new Fixture();
        var service = Substitute.For<IGameService>();
        var handler = new StartCommandHandler(service);
        var settings = new GameSettings();
        var request = new StartCommandRequest(settings);
        var cancellationToken = CancellationToken.None;
        var game = fixture.Create<Game>();
        var result = status switch
        {
            ResultStatus.Accepted => Result<Game>.Accepted(game),
            ResultStatus.Conflict => Result<Game>.Conflict(),
            ResultStatus.Unprocessable => Result<Game>.Unprocessable("unprocessable"),
            ResultStatus.Error => Result<Game>.Error("error"),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        service.StartAsync(Arg.Any<GameSettings>(), Arg.Any<CancellationToken>()).Returns(result);

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        await service.Received(1).StartAsync(settings, cancellationToken);
        response.Should().BeEquivalentTo(result.ToResult(response.Value));
    }
}
