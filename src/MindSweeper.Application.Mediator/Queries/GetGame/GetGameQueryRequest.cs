namespace MindSweeper.Application.Mediator.Queries.GetGame;

/// <summary>
/// Represents a request to get a game by player ID.
/// </summary>
public record GetGameQueryRequest() : IRequest<Result<GetGameQueryResponse>>;
