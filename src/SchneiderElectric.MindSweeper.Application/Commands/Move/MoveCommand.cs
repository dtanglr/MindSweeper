using MediatR;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public class MoveCommand : IRequest
{
    public Guid GameId { get; init; }

    public Direction Direction { get; init; }
}
