using MediatR;
using MindSweeper.Domain;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Queries.GetGame;

/// <summary>
/// Request handler for getting the game for the player.
/// </summary>
public class GetGameQueryHandler(PlayerContext context) : IRequestHandler<GetGameQuery, Result<GetGameQueryResponse>>
{
    private readonly PlayerContext _context = context;

    /// <summary>
    /// Handles the GetGameRequest and returns the result.
    /// </summary>
    /// <param name="request">The GetGameRequest object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the GetGameRequest.</returns>
    public Task<Result<GetGameQueryResponse>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        if (!_context.HasGame)
        {
            var result = Result<GetGameQueryResponse>.NotFound();

            return Task.FromResult(result);
        }
        else
        {
            var game = _context.Game!;
            var response = new GetGameQueryResponse(game);
            var result = Result<GetGameQueryResponse>.Success(response);

            return Task.FromResult(result);
        }
    }
}
