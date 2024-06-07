using MindSweeper.Domain.Results;

namespace MindSweeper.Domain.UnitTests.GameServiceTests;

public class EndAsyncTests
{
    [Fact]
    public async Task EndAsync_WithoutExistingGame_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();
        var repository = Substitute.For<IGameRepository>();
        var service = new GameService(context, repository);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result.NotFound());
    }

    [Fact]
    public async Task EndAsync_WithFailedPersistenceAttempt_ReturnsError()
    {
        // Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext(game.PlayerId) { Game = game };

        var repository = Substitute.For<IGameRepository>();
        repository.DeleteGameAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Result.Error());

        var service = new GameService(context, repository);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        await repository.Received(1).DeleteGameAsync(game.Id, Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error());
    }

    [Fact]
    public async Task EndAsync_WithException_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext(game.PlayerId) { Game = game };

        var repository = Substitute.For<IGameRepository>();
        repository.DeleteGameAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ThrowsAsync(new Exception(ErrorMessage));

        var service = new GameService(context, repository);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        await repository.Received(1).DeleteGameAsync(game.Id, Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }

    [Fact]
    public async Task EndAsync_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext(game.PlayerId) { Game = game };

        var repository = Substitute.For<IGameRepository>();
        repository.DeleteGameAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Result.Accepted());

        var service = new GameService(context, repository);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        await repository.Received(1).DeleteGameAsync(game.Id, Arg.Any<CancellationToken>());
        result.Status.Should().Be(ResultStatus.Accepted);
    }
}
