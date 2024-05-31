﻿using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Handles the MoveCommand and updates the game state accordingly.
/// </summary>
public class MoveCommandHandler : IRequestHandler<MoveCommand, Result<MoveCommandResponse>>
{
    private readonly IGameService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandHandler"/> class.
    /// </summary>
    /// <param name="service">The game repository.</param>
    public MoveCommandHandler(IGameService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the MoveCommand and updates the game state accordingly.
    /// </summary>
    /// <param name="request">The MoveCommand request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the MoveCommand handling.</returns>
    public async Task<Result<MoveCommandResponse>> Handle(MoveCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.MoveAsync(request.Direction, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ToResult<MoveCommandResponse>();
        }

        var response = new MoveCommandResponse(result.Value!);

        return Result<MoveCommandResponse>.Accepted(response);
    }
}
