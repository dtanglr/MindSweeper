using System.IO.Abstractions;
using System.Text.Json;
using MindSweeper.Domain;
using MindSweeper.Domain.Results;

namespace MindSweeper.Persistence.LocalFile;

/// <summary>
/// Represents a JSON file game repository.
/// </summary>
/// <remarks>
/// This was originally developed as a multi-game repository to emulate a database, but it was later decided to use a single game repository.
/// </remarks>
public class JsonFileMultiGameRepository : IGameRepository
{
    private readonly IFileSystem _fileSystem;
    private readonly Lazy<string> _file;
    private readonly JsonSerializerOptions _options = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFileGameRepository"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="options">The JSON serializer options.</param>
    public JsonFileMultiGameRepository(IFileSystem fileSystem, JsonSerializerOptions? options = null)
    {
        _fileSystem = fileSystem;
        _file = new(() => _fileSystem.Path.Combine(_fileSystem.Directory.GetCurrentDirectory(), "game.json"));
        _options = options ?? _options;
    }

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

            if (JsonFileExists())
            {
                await foreach (var existingGame in JsonFileData(cancellationToken))
                {
                    if (existingGame!.PlayerId == newGame.PlayerId)
                    {
                        return Result.Conflict();
                    }

                    games.Add(existingGame);
                }
            }

            games.Add(newGame);

            await WriteJsonFileData(games, cancellationToken);

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
            if (JsonFileExists())
            {
                var games = new List<Game>();
                var existed = false;

                await foreach (var existingGame in JsonFileData(cancellationToken))
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
                    await WriteJsonFileData(games, cancellationToken);

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
            if (JsonFileExists())
            {
                await foreach (var game in JsonFileData(cancellationToken))
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
            if (JsonFileExists())
            {
                var games = new List<Game>();
                var exists = false;

                await foreach (var existingGame in JsonFileData(cancellationToken))
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
                    await WriteJsonFileData(games, cancellationToken);

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
    /// Gets the data of the JSON file.
    /// </summary>
    /// <returns>The byte array containing the JSON file data.</returns>
    private byte[] ReadJsonFileData()
    {
        return _fileSystem.File.ReadAllBytes(_file.Value);
    }

    /// <summary>
    /// Retrieves the data from the JSON file as an asynchronous enumerable of Game objects.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous enumerable of Game objects.</returns>
    private IAsyncEnumerable<Game?> JsonFileData(CancellationToken cancellationToken)
    {
        var data = ReadJsonFileData();

        return JsonSerializer.DeserializeAsyncEnumerable<Game>(new MemoryStream(data), _options, cancellationToken);
    }

    /// <summary>
    /// Determines whether the JSON file exists.
    /// </summary>
    /// <returns><c>true</c> if the JSON file exists; otherwise, <c>false</c>.</returns>
    private bool JsonFileExists()
    {
        return _fileSystem.File.Exists(_file.Value);
    }

    /// <summary>
    /// Writes the JSON file data asynchronously.
    /// </summary>
    /// <param name="games">The list of games to write.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private Task WriteJsonFileData(List<Game> games, CancellationToken cancellationToken)
    {
        return _fileSystem.File.WriteAllBytesAsync(_file.Value, JsonSerializer.SerializeToUtf8Bytes(games, _options), cancellationToken);
    }
}
