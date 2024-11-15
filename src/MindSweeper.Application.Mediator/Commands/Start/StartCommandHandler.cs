﻿namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Command handler for starting the game.
/// </summary>
public class StartCommandHandler : IRequestHandler<StartCommandRequest, Result<StartCommandResponse>>
{
    private readonly IGameService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandHandler"/> class.
    /// </summary>
    /// <param name="service">The game service.</param>
    public StartCommandHandler(IGameService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the StartCommand request.
    /// </summary>
    /// <param name="request">The StartCommand request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the StartCommand request.</returns>
    public async Task<Result<StartCommandResponse>> Handle(StartCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.StartAsync(request.Settings, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ToResult<StartCommandResponse>();
        }

        var response = new StartCommandResponse(result.Value!);

        return result.ToResult(response);
    }
}
