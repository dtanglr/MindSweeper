using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Represents the behavior for retrieving the game of a player in the move command pipeline.
/// </summary>
public sealed class MoveCommandGetGameBehavior : IPipelineBehavior<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandGetGameBehavior"/> class.
    /// </summary>
    /// <param name="repository">The game repository.</param>
    public MoveCommandGetGameBehavior(IGameRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the move command by retrieving the game of the player.
    /// </summary>
    /// <param name="request">The move command.</param>
    /// <param name="next">The next handler delegate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the move command.</returns>
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
