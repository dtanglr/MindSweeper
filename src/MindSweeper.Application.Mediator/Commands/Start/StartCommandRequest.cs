using MediatR;
using MindSweeper.Domain;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Represents a command to start the game.
/// </summary>
public record StartCommandRequest(GameSettings Settings) : IRequest<Result<StartCommandResponse>>;
