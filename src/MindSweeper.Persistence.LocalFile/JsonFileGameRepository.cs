﻿using System.IO.Abstractions;
using System.Text.Json;
using MindSweeper.Domain;
using MindSweeper.Domain.Results;

namespace MindSweeper.Persistence.LocalFile;

/// <summary>
/// Represents a JSON file game repository.
/// </summary>
public class JsonFileGameRepository : IGameRepository
{
    private readonly IFileSystem _fileSystem;
    private readonly Lazy<string> _file;
    private readonly JsonSerializerOptions _options = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFileGameRepository"/> class.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="options">The JSON serializer options.</param>
    public JsonFileGameRepository(IFileSystem fileSystem, JsonSerializerOptions? options = null)
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
            if (JsonFileExists())
            {
                var game = await GetJsonFileDataAsync(cancellationToken);
                var exists = game is not null;

                if (exists)
                {
                    return Result.Conflict();
                }
            }

            await WriteJsonFileDataAsync(newGame, cancellationToken);

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
    /// <param name="gameId">The ID of the game to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the result of the operation.</returns>
    public async Task<Result> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken)
    {
        try
        {
            if (JsonFileExists())
            {
                var game = await GetJsonFileDataAsync(cancellationToken);
                var exists = game?.Id == gameId;

                if (exists)
                {
                    await WriteEmptyJsonFileDataAsync(cancellationToken);

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
                var game = await GetJsonFileDataAsync(cancellationToken);
                var exists = game?.PlayerId == playerId;

                if (exists)
                {
                    return Result<Game>.Success(game!);
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
                var game = await GetJsonFileDataAsync(cancellationToken);
                var exists = game?.Id == updatedGame.Id;

                if (exists)
                {
                    await WriteJsonFileDataAsync(updatedGame, cancellationToken);

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
    /// Gets the JSON file data asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with the JSON file data.</returns>
    private ValueTask<Game?> GetJsonFileDataAsync(CancellationToken cancellationToken)
    {
        var data = ReadJsonFileData();

        return JsonSerializer.DeserializeAsync<Game>(new MemoryStream(data), _options, cancellationToken);
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
    /// Gets the data of the JSON file.
    /// </summary>
    /// <returns>The byte array containing the JSON file data.</returns>
    private byte[] ReadJsonFileData()
    {
        return _fileSystem.File.ReadAllBytes(_file.Value);
    }
    /// <summary>
    /// Writes an empty JSON file data asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private Task WriteEmptyJsonFileDataAsync(CancellationToken cancellationToken)
    {
        return _fileSystem.File.WriteAllBytesAsync(_file.Value, Array.Empty<byte>(), cancellationToken);
    }

    /// <summary>
    /// Writes the JSON file data asynchronously.
    /// </summary>
    /// <param name="game">The game data to write.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private Task WriteJsonFileDataAsync(Game game, CancellationToken cancellationToken)
    {
        return _fileSystem.File.WriteAllBytesAsync(_file.Value, JsonSerializer.SerializeToUtf8Bytes(game, _options), cancellationToken);
    }
}
