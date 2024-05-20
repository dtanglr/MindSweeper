using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public sealed class MoveCommandGameBehavior(IGameRepository repository) : IPipelineBehavior<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result<MoveCommandResponse>> Handle(MoveCommand request, RequestHandlerDelegate<Result<MoveCommandResponse>> next, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetGameAsync(request.PlayerId, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToResult<MoveCommandResponse>();
            }

            request.Game = result.Value!;

            var response = await next();

            return response;
        }
        catch (Exception ex)
        {
            return Result<MoveCommandResponse>.Error(ex.Message);
        }
    }
}
