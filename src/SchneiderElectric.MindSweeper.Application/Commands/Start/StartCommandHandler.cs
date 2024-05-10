using MediatR;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public class StartCommandHandler : IRequestHandler<StartCommand>
{
    public Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
