using MediatR;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Commands.End;

/// <summary>
/// Represents a command to end the game for the current player.
/// </summary>
public record EndCommand() : IRequest<Result>;
