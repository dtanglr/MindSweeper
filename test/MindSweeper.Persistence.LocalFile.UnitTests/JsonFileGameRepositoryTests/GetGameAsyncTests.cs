namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

/// <summary>
/// Unit tests for the GetGameAsync method in the JsonFileGameRepository class.
/// </summary>
public class GetGameAsyncTests : JsonFileGameRepositoryTests
{
    /// <summary>
    /// Tests the scenario when the JSON file is missing.
    /// </summary>
    [Fact]
    public async Task GetGameAsync_WhenJsonFileMissing_ReturnsNotFound()
    {
        // Arrange
        var playerId = "playerId";
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = false;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.GetGameAsync(playerId, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(0).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result<Game>.NotFound());
    }

    /// <summary>
    /// Tests the scenario when the JSON file exists but the game has a non-matching ID.
    /// </summary>
    [Fact]
    public async Task GetGameAsync_WhenJsonFileExists_WithAnExistingGame_WithNonMatchingId_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var playerId = "playerId";
        var existingGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.GetGameAsync(playerId, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result<Game>.NotFound());
    }

    /// <summary>
    /// Tests the scenario when the JSON file exists and the game has a matching ID.
    /// </summary>
    [Fact]
    public async Task GetGameAsync_WhenJsonFileExists_WithAnExistingGame_WithMatchingId_ReturnsSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        var existingGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.GetGameAsync(existingGame.PlayerId, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result<Game>.Success(existingGame!));
    }

    /// <summary>
    /// Tests the scenario when the JSON file exists but contains invalid content that causes a JsonException.
    /// </summary>
    [Fact]
    public async Task GetGameAsync_WhenJsonFileExists_WithInvalidContent_AJsonExceptionOccurs_ReturnsUnprocessable()
    {
        // Arrange
        var playerId = "playerId";
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(string.Empty, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.GetGameAsync(playerId, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Status.Should().Be(ResultStatus.Unprocessable);
        result.Errors.Count.Should().Be(1);
    }

    /// <summary>
    /// Tests the scenario when an exception occurs while writing the JSON file.
    /// </summary>
    [Fact]
    public async Task GetGameAsync_WhenWritingJsonFile_AnExceptionOccurs_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var existingGame = fixture.Create<Game>();
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Throws(new Exception(ErrorMessage));
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.GetGameAsync(existingGame.PlayerId, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }
}
