using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.End;

public class EndCommandHandler(IGameRepository repository) : IRequestHandler<EndCommand, Result>
{
    private readonly IGameRepository _repository = repository;

    public Task<Result> Handle(EndCommand request, CancellationToken cancellationToken)
    {
        return _repository.DeleteGameAsync(request.PlayerId, cancellationToken);
    }
}
