namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

/// <summary>
/// Unit tests for the CreateGameAsync method in the JsonFileGameRepository class.
/// </summary>
public class CreateGameAsyncTests : JsonFileGameRepositoryTests
{
    /// <summary>
    /// Tests the CreateGameAsync method when the JSON file exists and contains an existing game.
    /// It should return a conflict result.
    /// </summary>
    [Fact]
    public async Task CreateGameAsync_WhenJsonFileExists_WithAnExistingGame_ReturnsConflict()
    {
        // Arrange
        var fixture = new Fixture();
        var newGame = fixture.Create<Game>();
        var existingGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.CreateGameAsync(newGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Conflict());
    }

    /// <summary>
    /// Tests the CreateGameAsync method when the JSON file exists and does not contain an existing game.
    /// It should return an accepted result.
    /// </summary>
    [Fact]
    public async Task CreateGameAsync_WhenJsonFileExists_WithNoExistingGame_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var newGame = fixture.Create<Game>();
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.CreateGameAsync(newGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Accepted());
    }

    /// <summary>
    /// Tests the CreateGameAsync method when the JSON file is missing.
    /// It should return an accepted result.
    /// </summary>
    [Fact]
    public async Task CreateGameAsync_WhenJsonFileMissing_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var newGame = fixture.Create<Game>();
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = false;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.CreateGameAsync(newGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(0).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Accepted());
    }

    /// <summary>
    /// Tests the CreateGameAsync method when the JSON file exists but contains invalid content,
    /// resulting in a JsonException. It should return an unprocessable result.
    /// </summary>
    [Fact]
    public async Task CreateGameAsync_WhenJsonFileExists_WithInvalidContent_AJsonExceptionOccurs_ReturnsUnprocessable()
    {
        // Arrange
        var fixture = new Fixture();
        var newGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(string.Empty, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.CreateGameAsync(newGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Status.Should().Be(ResultStatus.Unprocessable);
        result.Errors.Count.Should().Be(1);
    }

    /// <summary>
    /// Tests the CreateGameAsync method when an exception occurs while writing the JSON file.
    /// It should return an error result.
    /// </summary>
    [Fact]
    public async Task CreateGameAsync_WhenWritingJsonFile_AnExceptionOccurs_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var newGame = fixture.Create<Game>();
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = false;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).ThrowsAsync(new Exception(ErrorMessage));

        // Act
        var result = await Repository.CreateGameAsync(newGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(0).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }
}
