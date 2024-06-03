using AutoFixture;
using MindSweeper.Domain.Results;
using Moq;

namespace MindSweeper.Domain.UnitTests.GameServiceTests;

public class EndAsyncTests
{
    [Fact]
    public async Task EndAsync_WithoutExistingGame_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();
        var repository = new Mock<IGameRepository>();
        var service = new GameService(context, repository.Object);

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

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.DeleteGameAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Error());

        var service = new GameService(context, repository.Object);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        repository.Verify(x => x.DeleteGameAsync(game.Id, It.IsAny<CancellationToken>()), Times.Once);
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

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.DeleteGameAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(ErrorMessage));

        var service = new GameService(context, repository.Object);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        repository.Verify(x => x.DeleteGameAsync(game.Id, It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }

    [Fact]
    public async Task EndAsync_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext(game.PlayerId) { Game = game };

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.DeleteGameAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Accepted());

        var service = new GameService(context, repository.Object);

        // Act
        var result = await service.EndAsync(CancellationToken.None);

        // Assert
        repository.Verify(x => x.DeleteGameAsync(game.Id, It.IsAny<CancellationToken>()), Times.Once);
        result.Status.Should().Be(ResultStatus.Accepted);
    }
}
