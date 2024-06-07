using MindSweeper.Domain.Components;
using MindSweeper.Domain.Results;

namespace MindSweeper.Domain.UnitTests.GameServiceTests;

public class MoveAsyncTests
{
    [Fact]
    public async Task MoveAsync_WithoutExistingGame_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var context = fixture.Create<PlayerContext>();
        var repository = Substitute.For<IGameRepository>();
        var service = new GameService(context, repository);

        // Act
        var result = await service.MoveAsync(Direction.Up, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result<Game>.NotFound());
    }

    [Fact]
    public async Task MoveAsync_WithFailedMoveAttempt_ReturnsUnprocessable()
    {
        // Arrange
        var fixture = new Fixture();

        var game = fixture.Build<Game>()
            .With(g => g.Settings, new GameSettings())
            .With(g => g.CurrentSquare, "A1")
            .Create();

        var context = new PlayerContext(game.PlayerId) { Game = game };
        var repository = Substitute.For<IGameRepository>();
        var service = new GameService(context, repository);
        var impossibleMoveDirection = Direction.Down;

        // Act
        var result = await service.MoveAsync(impossibleMoveDirection, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(Result<Game>.Unprocessable());
    }

    [Theory]
    [InlineData(Direction.Up, "A1", "A2", true)]
    [InlineData(Direction.Up, "A7", "A8", false)] // Move to last row to end game
    public async Task MoveAsync_WithInProgressGame_ReturnsAccepted(Direction direction, string @from, string @to, bool shouldBeInProgressAfterMove)
    {
        // Arrange
        var fixture = new Fixture();
        var settings = new GameSettings();
        var squares = new Field.Squares(settings);
        var bombs = new Field.Bombs(settings);
        var fromSquare = squares[@from];
        var toSquare = squares[@to];

        var game = fixture.Build<Game>()
            .With(g => g.Settings, settings)
            .With(g => g.CurrentSquare, fromSquare.Name)
            .With(g => g.AvailableMoves, fromSquare.GetAvailableMoves())
            .With(g => g.Bombs, bombs)
            .Without(g => g.Moves)
            .Without(g => g.LastMove)
            .Without(g => g.MovesMade)
            .Without(g => g.BombsHit)
            .With(g => g.Lives, settings.Lives)
            .With(g => g.Status, GameStatus.InProgress)
            .Create();

        var context = new PlayerContext(game.PlayerId) { Game = game };

        var repository = Substitute.For<IGameRepository>();
        repository.UpdateGameAsync(Arg.Any<Game>(), Arg.Any<CancellationToken>()).Returns(Result.Accepted());
        repository.DeleteGameAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Result.Accepted());

        var service = new GameService(context, repository);

        // Act
        var result = await service.MoveAsync(direction, CancellationToken.None);
        var updatedGame = context.Game!;
        var lastMove = updatedGame.LastMove;
        var hitBomb = lastMove!.HitBomb;

        // Assert
        await repository.Received(shouldBeInProgressAfterMove ? 1 : 0).UpdateGameAsync(Arg.Any<Game>(), Arg.Any<CancellationToken>());
        await repository.Received(shouldBeInProgressAfterMove ? 0 : 1).DeleteGameAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

        result.Status.Should().Be(ResultStatus.Accepted);

        lastMove.Should().NotBeNull();
        lastMove!.Direction.Should().Be(direction);
        lastMove!.FromSquare.Should().Be(fromSquare.Name);
        lastMove!.ToSquare.Should().Be(toSquare.Name);

        game.Moves.Should().Be(0);
        game.MovesMade.Should().BeEmpty();
        game.AvailableMoves.Should().BeEquivalentTo(fromSquare.GetAvailableMoves());
        game.BombsHit.Should().Be(0);
        game.Lives.Should().Be(settings.Lives);

        updatedGame.Moves.Should().Be(1);
        updatedGame.MovesMade.Should().OnlyContain(m => m == lastMove);
        updatedGame.AvailableMoves.Should().BeEquivalentTo(toSquare.GetAvailableMoves());
        updatedGame.BombsHit.Should().Be(hitBomb ? 1 : 0);
        updatedGame.Lives.Should().Be(hitBomb ? settings.Lives - 1 : settings.Lives);

        if (shouldBeInProgressAfterMove)
        {
            updatedGame.Status.Should().Be(GameStatus.InProgress);
        }
        else
        {
            updatedGame.Status.Should().BeOneOf(GameStatus.Won, GameStatus.Lost);
        }
    }
}
