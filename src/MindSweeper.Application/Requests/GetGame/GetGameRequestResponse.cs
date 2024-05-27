using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Represents the response for the GetGameRequest.
/// </summary>
public record GetGameRequestResponse(Game Game);
