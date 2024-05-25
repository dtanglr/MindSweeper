using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.End;

public class EndCommandHandler(IGameRepository repository) : IRequestHandler<EndCommand, Result>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result> Handle(EndCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.DeleteGameAsync(request.PlayerId, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
