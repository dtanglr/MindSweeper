using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

public record MoveCommand(string PlayerId, Direction Direction) : IRequest<Result<MoveCommandResponse>>
{
    internal Game? Game { get; set; }
}
