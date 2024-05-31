using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Queries.GetGame;

/// <summary>
/// Represents a request to get a game by player ID.
/// </summary>
public record GetGameQuery() : IRequest<Result<GetGameQueryResponse>>;
