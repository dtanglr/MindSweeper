namespace MindSweeper.Application.Mediator.Commands.End;

/// <summary>
/// Command handler for ending the game.
/// </summary>
public class EndCommandHandler : IRequestHandler<EndCommandRequest, Result>
{
    private readonly IGameService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandHandler"/> class.
    /// </summary>
    /// <param name="service">The game repository.</param>
    public EndCommandHandler(IGameService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the end command.
    /// </summary>
    /// <param name="request">The end command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public Task<Result> Handle(EndCommandRequest request, CancellationToken cancellationToken)
    {
        return _service.EndAsync(cancellationToken);
    }
}
