using MediatR;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public class MoveCommandHandler : IRequestHandler<MoveCommand>
{
    public Task Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
