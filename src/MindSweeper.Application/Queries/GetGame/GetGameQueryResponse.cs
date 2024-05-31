using MindSweeper.Domain;

namespace MindSweeper.Application.Queries.GetGame;

/// <summary>
/// Represents the response for the GetGameRequest.
/// </summary>
public record GetGameQueryResponse(Game Game);
