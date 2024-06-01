using MediatR;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Mediator.Queries.GetGame;

/// <summary>
/// Represents a request to get a game by player ID.
/// </summary>
public record GetGameQuery() : IRequest<Result<GetGameQueryResponse>>;
