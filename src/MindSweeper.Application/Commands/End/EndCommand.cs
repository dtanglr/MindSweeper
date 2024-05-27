using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.End;

/// <summary>
/// Represents a command to end the game for a specific player.
/// </summary>
public record EndCommand(string PlayerId) : IRequest<Result>;
