using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Request handler for getting the game for the player.
/// </summary>
public class GetGameRequestHandler(PlayerContext context) : IRequestHandler<GetGameRequest, Result<GetGameRequestResponse>>
{
    private readonly PlayerContext _context = context;

    /// <summary>
    /// Handles the GetGameRequest and returns the result.
    /// </summary>
    /// <param name="request">The GetGameRequest object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the GetGameRequest.</returns>
    public Task<Result<GetGameRequestResponse>> Handle(GetGameRequest request, CancellationToken cancellationToken)
    {
        if (!_context.HasGame)
        {
            var result = Result<GetGameRequestResponse>.NotFound();

            return Task.FromResult(result);
        }
        else
        {
            var game = _context.Game!;
            var response = new GetGameRequestResponse(game);
            var result = Result<GetGameRequestResponse>.Success(response);

            return Task.FromResult(result);
        }
    }
}
