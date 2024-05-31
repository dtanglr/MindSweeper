using System.Text.Json;
using MindSweeper.Domain;

namespace MindSweeper.Persistence;

/// <summary>
/// Represents a JSON file game repository.
/// </summary>
public class JsonFileGameRepository : IGameRepository
{
    private const string FileName = "game.json";

    private static readonly string _file = Path.Combine(Directory.GetCurrentDirectory(), FileName);
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Creates a new game asynchronously.
    /// </summary>
    /// <param name="newGame">The new game to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the result of the operation.</returns>
    public async Task<Result> CreateGameAsync(Game newGame, CancellationToken cancellationToken)
    {
        try
        {
            var games = new List<Game>();

            if (File.Exists(_file))
            {
                var data = File.ReadAllBytes(_file);
                using var stream = new MemoryStream(data);

                await foreach (var existingGame in JsonSerializer.DeserializeAsyncEnumerable<Game>(stream, _options, cancellationToken))
                {
                    if (existingGame!.PlayerId == newGame.PlayerId)
                    {
                        return Result.Conflict();
                    }

                    games.Add(existingGame);
                }
            }

            games.Add(newGame);

            await File.WriteAllBytesAsync(_file, JsonSerializer.SerializeToUtf8Bytes(games, _options), cancellationToken);
            return Result.Accepted();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a game asynchronously.
    /// </summary>
    /// <param name="gameId">The player ID of the game to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the result of the operation.</returns>
    public async Task<Result> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(_file))
            {
                var data = File.ReadAllBytes(_file);
                using var stream = new MemoryStream(data);
                var games = new List<Game>();
                var existed = false;

                await foreach (var existingGame in JsonSerializer.DeserializeAsyncEnumerable<Game>(stream, _options, cancellationToken))
                {
                    if (existingGame!.Id == gameId)
                    {
                        existed = true;
                        continue;
                    }

                    games.Add(existingGame);
                }

                if (existed)
                {
                    await File.WriteAllBytesAsync(_file, JsonSerializer.SerializeToUtf8Bytes(games, _options), cancellationToken);
                    return Result.Accepted();
                }
            }

            return Result.NotFound();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    /// <summary>
    /// Gets a game asynchronously.
    /// </summary>
    /// <param name="playerId">The player ID of the game to get.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the result of the operation.</returns>
    public async Task<Result<Game>> GetGameAsync(string playerId, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(_file))
            {
                var data = File.ReadAllBytes(_file);
                using var stream = new MemoryStream(data);

                await foreach (var game in JsonSerializer.DeserializeAsyncEnumerable<Game>(stream, _options, cancellationToken))
                {
                    if (game!.PlayerId == playerId)
                    {
                        return Result<Game>.Success(game);
                    }
                }
            }

            return Result<Game>.NotFound();
        }
        catch (Exception ex)
        {
            return Result<Game>.Error(ex.Message);
        }
    }

    /// <summary>
    /// Updates a game asynchronously.
    /// </summary>
    /// <param name="updatedGame">The updated game.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the result of the operation.</returns>
    public async Task<Result> UpdateGameAsync(Game updatedGame, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(_file))
            {
                var data = File.ReadAllBytes(_file);
                using var stream = new MemoryStream(data);
                var games = new List<Game>();
                var exists = false;

                await foreach (var existingGame in JsonSerializer.DeserializeAsyncEnumerable<Game>(stream, _options, cancellationToken))
                {
                    if (existingGame!.PlayerId == updatedGame.PlayerId)
                    {
                        exists = true;
                        games.Add(updatedGame);
                        continue;
                    }

                    games.Add(existingGame);
                }

                if (exists)
                {
                    await File.WriteAllBytesAsync(_file, JsonSerializer.SerializeToUtf8Bytes(games, _options), cancellationToken);
                    return Result.Accepted();
                }
            }

            return Result.NotFound();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
