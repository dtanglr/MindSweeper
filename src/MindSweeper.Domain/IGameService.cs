namespace MindSweeper.Domain;

/// <summary>
/// Represents a game service interface.
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Ends the game asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result> EndAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the current game asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<Game>> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Moves the game in the specified direction asynchronously.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<Game>> MoveAsync(Direction direction, CancellationToken cancellationToken);

    /// <summary>
    /// Starts a new game with the specified settings asynchronously.
    /// </summary>
    /// <param name="settings">The game settings.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<Game>> StartAsync(Settings settings, CancellationToken cancellationToken);
}

public class GameService : IGameService
{
    private readonly PlayerContext _context;
    private readonly IGameRepository _repository;

    public GameService(PlayerContext context, IGameRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Result> EndAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.DeleteGameAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<Game>> GetAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.GetGameAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }

    public async Task<Result<Game>> MoveAsync(Direction direction, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetGameAsync(cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToResult<Game>();
            }

            var game = result.Value!;

            game.Move(direction);

            return await _repository.UpdateGameAsync(game, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }

    public async Task<Result<Game>> StartAsync(Settings settings, CancellationToken cancellationToken)
    {
        try
        {
            var newGame = new Game(settings);

            return await _repository.CreateGameAsync(newGame, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }
}
