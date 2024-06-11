using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MindSweeper.Application.Mediator.Behaviors;

/// <summary>
/// Represents a behavior that sets the player context before processing a request.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
public sealed class PlayerContextBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : class
{
    private readonly PlayerContext _context;
    private readonly IGameRepository _repository;
    private readonly ILogger<PlayerContextBehavior<TRequest>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerContextBehavior{TRequest}"/> class.
    /// </summary>
    /// <param name="context">The player context.</param>
    /// <param name="repository">The game repository.</param>
    /// <param name="logger">The logger.</param>
    public PlayerContextBehavior(PlayerContext context, IGameRepository repository, ILogger<PlayerContextBehavior<TRequest>> logger)
    {
        if (string.IsNullOrEmpty(context.Id))
        {
            throw new ArgumentException("Player ID cannot be null or empty.", nameof(context));
        }

        _context = context;
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Processes the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetGameAsync(_context.Id, cancellationToken);
            _context.Game = result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred getting the game for player: {PlayerId}.", _context.Id);
        }
    }
}
