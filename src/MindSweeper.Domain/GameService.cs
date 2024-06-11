using MindSweeper.Domain.Components;
using MindSweeper.Domain.Results;

namespace MindSweeper.Domain;

/// <summary>
/// Represents a service for managing the game.
/// </summary>
public class GameService : IGameService
{
    private readonly PlayerContext _context;
    private readonly IGameRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameService"/> class.
    /// </summary>
    /// <param name="context">The player context.</param>
    /// <param name="repository">The game repository.</param>
    public GameService(PlayerContext context, IGameRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    /// <summary>
    /// Starts a new game with the specified settings.
    /// </summary>
    /// <param name="settings">The game settings.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<Game>> StartAsync(GameSettings settings, CancellationToken cancellationToken)
    {
        if (_context.HasGame)
        {
            return Result<Game>.Conflict();
        }

        try
        {
            var id = Guid.NewGuid();
            var playerId = _context.Id;
            var squares = new Field.Squares(settings);
            var bombs = new Field.Bombs(settings);
            var startSquare = squares.GetStartSquare();
            var game = new Game
            {
                Id = id,
                PlayerId = playerId,
                Settings = settings,
                Bombs = bombs.ToList(),
                CurrentSquare = startSquare.Name,
                AvailableMoves = startSquare.GetAvailableMoves(),
                Status = GameStatus.InProgress,
                Lives = settings.Lives
            };

            var result = await _repository.CreateGameAsync(game, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ToResult<Game>();
            }

            _context.Game = game;

            return Result<Game>.Accepted(game);
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Moves the player in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the move operation.</returns>
    public async Task<Result<Game>> MoveAsync(Direction direction, CancellationToken cancellationToken)
    {
        if (!_context.HasGame)
        {
            return Result<Game>.NotFound();
        }

        try
        {
            var game = _context.Game!.DeepCopy();
            var squares = new Field.Squares(game.Settings);
            var fromSquare = squares[game.CurrentSquare];

            if (!fromSquare.TryMove(direction, out var toSquare))
            {
                return Result<Game>.Forbidden();
            }

            var bombs = new Field.Bombs(game.Bombs);
            var hitBomb = toSquare!.HasBomb(bombs);
            var move = new Move(direction, fromSquare.Name, toSquare.Name, hitBomb);
            game.Moves++;
            game.LastMove = move;
            game.MovesMade.Add(move);
            game.CurrentSquare = toSquare.Name;
            game.AvailableMoves = toSquare.GetAvailableMoves();
            game.BombsHit += hitBomb ? 1 : 0;
            game.Lives -= hitBomb ? 1 : 0;
            game.Status = game.Lives == 0
                ? GameStatus.Lost : toSquare.IsOnLastRow
                    ? GameStatus.Won : GameStatus.InProgress;

            if (game.Status == GameStatus.InProgress)
            {
                var updateResult = await _repository.UpdateGameAsync(game, cancellationToken);

                if (!updateResult.IsSuccess)
                {
                    return updateResult.ToResult<Game>();
                }
            }
            else
            {
                var deleteResult = await _repository.DeleteGameAsync(game.Id, cancellationToken);

                if (!deleteResult.IsSuccess)
                {
                    return deleteResult.ToResult<Game>();
                }
            }

            _context.Game = game;

            return Result<Game>.Accepted(game);
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Ends the current game.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> EndAsync(CancellationToken cancellationToken)
    {
        if (!_context.HasGame)
        {
            return Result.NotFound();
        }

        try
        {
            var result = await _repository.DeleteGameAsync(_context.Game!.Id, cancellationToken);

            if (result.IsSuccess)
            {
                _context.Game = null;
            }

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
