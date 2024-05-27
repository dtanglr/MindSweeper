using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Represents a command to move the player in a game.
/// </summary>
public record MoveCommand(string PlayerId, Direction Direction) : IRequest<Result<MoveCommandResponse>>
{
    internal Game? Game { get; set; }
}
