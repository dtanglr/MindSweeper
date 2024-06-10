namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

/// <summary>
/// Represents a base class for unit tests of the <see cref="JsonFileGameRepository"/> class.
/// </summary>
public abstract class JsonFileGameRepositoryTests
{
    private const string Directory = "";
    private const string FileName = "game.json";

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFileGameRepositoryTests"/> class.
    /// </summary>
    public JsonFileGameRepositoryTests()
    {
        FilePath = FileName;

        FileSystem = Substitute.For<IFileSystem>();
        FileSystem.Directory.GetCurrentDirectory().Returns(Directory);
        FileSystem.Path.Combine(Arg.Any<string>(), Arg.Any<string>()).Returns(FilePath);

        SerializerOptions = new();

        Repository = new JsonFileGameRepository(FileSystem, SerializerOptions);
    }

    /// <summary>
    /// Gets the file path.
    /// </summary>
    protected string FilePath { get; }

    /// <summary>
    /// Gets the file system.
    /// </summary>
    protected IFileSystem FileSystem { get; }

    /// <summary>
    /// Gets the game repository.
    /// </summary>
    protected IGameRepository Repository { get; }

    /// <summary>
    /// Gets the serializer options.
    /// </summary>
    protected JsonSerializerOptions SerializerOptions { get; }
}
