using AutoFixture;
using MindSweeper.Domain.Components;
using MindSweeper.Domain.Results;
using Moq;

namespace MindSweeper.Domain.UnitTests.GameServiceTests;

public class StartAsyncTests
{
    [Fact]
    public async Task StartAsync_WithExistingGame_ReturnsConflict()
    {
        // Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext(game.PlayerId) { Game = game };
        var repository = new Mock<IGameRepository>();
        var service = new GameService(context, repository.Object);
        var settings = new GameSettings();

        // Act
        var result = await service.StartAsync(settings, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result<Game>.Conflict());
    }

    [Fact]
    public async Task StartAsync_WithFailedPersistenceAttempt_ReturnsError()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Error());

        var service = new GameService(context, repository.Object);
        var settings = new GameSettings();

        // Act
        var result = await service.StartAsync(settings, CancellationToken.None);

        // Assert
        repository.Verify(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeEquivalentTo(Result<Game>.Error());
    }

    [Fact]
    public async Task StartAsync_WithException_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(ErrorMessage));

        var service = new GameService(context, repository.Object);
        var settings = new GameSettings();

        // Act
        var result = await service.StartAsync(settings, CancellationToken.None);

        // Assert
        repository.Verify(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeEquivalentTo(Result<Game>.Error(ErrorMessage));
    }

    [Fact]
    public async Task StartAsync_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();

        var repository = new Mock<IGameRepository>();
        repository.Setup(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Accepted());

        var service = new GameService(context, repository.Object);
        var settings = new GameSettings();
        var squares = new Field.Squares(settings);

        // Act
        var result = await service.StartAsync(settings, CancellationToken.None);

        // Assert
        repository.Verify(x => x.CreateGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Status.Should().Be(ResultStatus.Accepted);
        context.Game.Should().NotBeNull();
        context.Game!.Id.Should().NotBe(Guid.Empty);
        context.Game!.PlayerId.Should().Be(context.Id);
        context.Game!.Settings.Should().Be(settings);
        context.Game!.Bombs.Should().NotBeEmpty();
        context.Game!.CurrentSquare.Should().NotBeEmpty();

        var currentSquare = squares[context.Game!.CurrentSquare];
        currentSquare.Row.Index.Should().Be(0);

        context.Game!.AvailableMoves.Should().NotBeEmpty();
        context.Game!.AvailableMoves.Should().BeEquivalentTo(currentSquare.GetAvailableMoves());
        context.Game!.Status.Should().Be(GameStatus.InProgress);
        context.Game!.Lives.Should().Be(settings.Lives);
    }
}
