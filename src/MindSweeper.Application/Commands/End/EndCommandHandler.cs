using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.End;

/// <summary>
/// Command handler for ending the game.
/// </summary>
public class EndCommandHandler : IRequestHandler<EndCommand, Result>
{
    private readonly IGameRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The game repository.</param>
    public EndCommandHandler(IGameRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the end command.
    /// </summary>
    /// <param name="request">The end command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(EndCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.DeleteGameAsync(request.PlayerId, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
