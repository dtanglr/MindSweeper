using MindSweeper.Application.Mediator.Queries.GetGame;

namespace MindSweeper.Application.Mediator.UnitTests.QueryTests.GetGame;

/// <summary>
/// Unit tests for the <see cref="GetGameQueryHandler"/> class.
/// </summary>
public class GetGameQueryHandlerTests
{
    /// <summary>
    /// Tests the <see cref="GetGameQueryHandler.Handle"/> method which get the player's current game and returns the expected result.
    /// </summary>
    /// <param name="status">The result status.</param>
    [Theory]
    [InlineData(ResultStatus.Ok)]
    [InlineData(ResultStatus.NotFound)]
    public async Task Handle_WhenServiceGetsGame_ReturnsExpectedResult(ResultStatus status)
    {
        // Arrange
        var fixture = new Fixture();
        var game = fixture.Create<Game>();
        var context = new PlayerContext("id");

        if (status == ResultStatus.Ok)
        {
            context.Game = game;
        }

        var handler = new GetGameQueryHandler(context);
        var request = new GetGameQueryRequest();
        var cancellationToken = CancellationToken.None;
        var result = status switch
        {
            ResultStatus.Ok => Result<GetGameQueryResponse>.Success(new(game)),
            ResultStatus.NotFound => Result<GetGameQueryResponse>.NotFound(),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        response.Should().BeEquivalentTo(result);
    }
}
