using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public class MoveCommandGameBehavior(IGameRepository repository) : IPipelineBehavior<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameRepository _repository = repository;

    public async Task<Result<MoveCommandResponse>> Handle(MoveCommand request, RequestHandlerDelegate<Result<MoveCommandResponse>> next, CancellationToken cancellationToken)
    {
        try
        {
            var getGameResult = await _repository.GetGameAsync(request.PlayerId, cancellationToken);

            if (!getGameResult.IsSuccess)
            {
                return getGameResult.ToResult<MoveCommandResponse>();
            }

            request.Game = getGameResult.Value!;

            var response = await next();

            return response;
        }
        catch (Exception ex)
        {
            return Result<MoveCommandResponse>.Error(ex.Message);
        }
    }
}
