using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Request handler for getting the game for the player.
/// </summary>
public class GetGameRequestHandler(IGameRepository repository) : IRequestHandler<GetGameRequest, Result<GetGameRequestResponse>>
{
    private readonly IGameRepository _repository = repository;

    /// <summary>
    /// Handles the GetGameRequest and returns the result.
    /// </summary>
    /// <param name="request">The GetGameRequest object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the GetGameRequest.</returns>
    public async Task<Result<GetGameRequestResponse>> Handle(GetGameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetGameAsync(request.PlayerId, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToResult<GetGameRequestResponse>();
            }

            var response = new GetGameRequestResponse(result.Value!);

            return Result<GetGameRequestResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<GetGameRequestResponse>.Error(ex.Message);
        }
    }
}
