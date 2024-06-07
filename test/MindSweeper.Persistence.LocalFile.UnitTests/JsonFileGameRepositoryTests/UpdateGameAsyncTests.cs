namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

public class UpdateGameAsyncTests : JsonFileGameRepositoryTests
{
    [Fact]
    public async Task UpdateGameAsync_WhenJsonFileMissing_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var updatedGame = fixture.Create<Game>();
        var existingGameInBytes = Array.Empty<byte>();
        var fileExists = false;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.UpdateGameAsync(updatedGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(0).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.NotFound());
    }

    [Fact]
    public async Task UpdateGameAsync_WhenJsonFileExists_WithAnExistingGame_WithNonMatchingId_ReturnsNotFound()
    {
        // Arrange
        var fixture = new Fixture();
        var updatedGame = fixture.Create<Game>();
        var existingGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.UpdateGameAsync(updatedGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.NotFound());
    }

    [Fact]
    public async Task UpdateGameAsync_WhenJsonFileExists_WithAnExistingGame_WithMatchingId_ReturnsAccepted()
    {
        // Arrange
        var fixture = new Fixture();
        var updatedGame = fixture.Create<Game>();
        var existingGame = fixture
            .Build<Game>()
            .With(g => g.Id, updatedGame.Id)
            .Create();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.DeleteGameAsync(updatedGame.Id, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Accepted());
    }

    [Fact]
    public async Task UpdateGameAsync_WhenJsonFileExists_WithInvalidContent_AJsonExceptionOccurs_ReturnsUnprocessable()
    {
        // Arrange
        var fixture = new Fixture();
        var updatedGame = fixture.Create<Game>();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(string.Empty, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await Repository.UpdateGameAsync(updatedGame, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(0).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Status.Should().Be(ResultStatus.Unprocessable);
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public async Task UpdateGameAsync_WhenWritingJsonFile_AnExceptionOccurs_ReturnsError()
    {
        // Arrange
        const string ErrorMessage = "error";
        var fixture = new Fixture();
        var updatedGame = fixture.Create<Game>();
        var existingGame = fixture
            .Build<Game>()
            .With(g => g.Id, updatedGame.Id)
            .Create();
        var existingGameInBytes = JsonSerializer.SerializeToUtf8Bytes(existingGame, SerializerOptions);
        var fileExists = true;
        FileSystem.File.Exists(Arg.Any<string>()).Returns(fileExists);
        FileSystem.File.ReadAllBytes(Arg.Any<string>()).Returns(existingGameInBytes);
        FileSystem.File.WriteAllBytesAsync(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<CancellationToken>()).ThrowsAsync(new Exception(ErrorMessage));

        // Act
        var result = await Repository.DeleteGameAsync(updatedGame.Id, CancellationToken.None);

        // Assert
        FileSystem.File.Received(1).Exists(FilePath);
        FileSystem.File.Received(1).ReadAllBytes(FilePath);
        await FileSystem.File.Received(1).WriteAllBytesAsync(FilePath, Arg.Any<byte[]>(), Arg.Any<CancellationToken>());
        result.Should().BeEquivalentTo(Result.Error(ErrorMessage));
    }
}
