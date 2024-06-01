using MindSweeper.Domain;

namespace MindSweeper.Application.Mediator.Queries.GetGame;

/// <summary>
/// Represents the response for the GetGameRequest.
/// </summary>
public record GetGameQueryResponse(Game Game);
