using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Represents a command to move the player in a game.
/// </summary>
public record MoveCommand(Direction Direction) : IRequest<Result<MoveCommandResponse>>;
