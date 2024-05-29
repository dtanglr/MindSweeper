using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Represents a request to get a game by player ID.
/// </summary>
public record GetGameRequest() : IRequest<Result<GetGameRequestResponse>>;
