using MELT;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Mediator.Behaviors;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MindSweeper.Application.Mediator.UnitTests.BehaviorTests;

/// <summary>
/// Unit tests for the PlayerContextBehavior class.
/// </summary>
public class PlayerContextBehaviorTests
{
    /// <summary>
    /// Tests the Process method when the repository returns a successful result.
    /// It should set the game in the player's context.
    /// </summary>
    [Fact]
    public async Task Process_WhenRepositoryReturnsSuccessfulResult_SetsGameInPlayerContext()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();
        var repository = Substitute.For<IGameRepository>();
        var logger = Substitute.For<ILogger<PlayerContextBehavior<object>>>();
        var behavior = new PlayerContextBehavior<object>(context, repository, logger);
        var request = new object();
        var cancellationToken = CancellationToken.None;
        var game = fixture.Create<Game>();
        var result = Result<Game>.Success(game);
        repository.GetGameAsync(Arg.Any<string>(), cancellationToken).Returns(result);

        // Act
        await behavior.Process(request, cancellationToken);

        // Assert
        await repository.Received(1).GetGameAsync(context.Id, cancellationToken);
        context.Game.Should().Be(game);
    }

    /// <summary>
    /// Tests the Process method when the repository throws an exception.
    /// It should log the error.
    /// </summary>
    [Fact]
    public async Task Process_WhenRepositoryThrowsException_LogsError()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();
        var repository = Substitute.For<IGameRepository>();
        var loggerFactory = TestLoggerFactory.Create();
        var logger = loggerFactory.CreateLogger<PlayerContextBehavior<object>>();
        var behavior = new PlayerContextBehavior<object>(context, repository, logger);
        var request = new object();
        var cancellationToken = CancellationToken.None;
        var exception = fixture.Create<Exception>();
        repository.GetGameAsync(Arg.Any<string>(), cancellationToken).ThrowsAsync(exception);

        // Act
        await behavior.Process(request, cancellationToken);

        // Assert
        await repository.Received(1).GetGameAsync(context.Id, cancellationToken);

        var log = Assert.Single(loggerFactory.Sink.LogEntries);
        log.Message.Should().Be($"An error occurred getting the game for player: {context.Id}.");
    }
}
