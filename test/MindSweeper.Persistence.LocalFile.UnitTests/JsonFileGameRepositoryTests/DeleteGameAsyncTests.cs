namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

/// <summary>
/// Tests the DeleteGameAsync method when the JSON file is missing and expects a NotFound result.
/// </summary>
public class DeleteGameAsyncTests : JsonFileGameRepositoryTests
{
    /// <summary>
    /// Tests the DeleteGameAsync method when the JSON file is missing and expects a NotFound result.
    /// </summary>
    [Fact]
    public async Task DeleteGameAsync_WhenJsonFileMissing_ReturnsNotFound()
    {
        // Arrange
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = false;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.DeleteGameAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(0).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.NotFound());
    }

    /// <summary>
    /// Tests the DeleteGameAsync method when the JSON file exists with an existing game but the IDs do not match, and expects a NotFound result.
    /// </summary>
    [Fact]
    public async Task DeleteGameAsync_WhenJsonFileExists_WithAnExistingGame_WithNonMatchingId_ReturnsNotFound()
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
        var result = await Repository.DeleteGameAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.NotFound());
    }

    /// <summary>
    /// Tests the DeleteGameAsync method when the JSON file exists with an existing game and the IDs match, and expects an Accepted result.
    /// </summary>
    [Fact]
    public async Task DeleteGameAsync_WhenJsonFileExists_WithAnExistingGame_WithMatchingId_ReturnsAccepted()
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
        var result = await Repository.DeleteGameAsync(existingGame.Id, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Accepted());
    }

    /// <summary>
    /// Tests the DeleteGameAsync method when the JSON file exists with invalid content and a JsonException occurs, and expects an Unprocessable result.
    /// </summary>
    [Fact]
    public async Task DeleteGameAsync_WhenJsonFileExists_WithInvalidContent_AJsonExceptionOccurs_ReturnsUnprocessable()
    {
        // Arrange
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(string.Empty, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.DeleteGameAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Status.Should().Be(ResultStatus.Unprocessable);
        result.Errors.Count.Should().Be(1);
    }

    /// <summary>
    /// Tests the DeleteGameAsync method when an exception occurs while writing the JSON file, and expects an Error result.
    /// </summary>
    [Fact]
    public async Task DeleteGameAsync_WhenWritingJsonFile_AnExceptionOccurs_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var existingGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).ThrowsAsync(new Exception(ErrorMessage));

        // Act
        var result = await Repository.DeleteGameAsync(existingGame.Id, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }
}
