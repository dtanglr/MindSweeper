namespace MindSweeper.Persistence.LocalFile.UnitTests.JsonFileGameRepositoryTests;

public abstract class JsonFileGameRepositoryTests
{
    private const string Directory = "";
    private const string FileName = "game.json";

    public JsonFileGameRepositoryTests()
    {
        FilePath = FileName;

        FileSystem = Substitute.For<IFileSystem>();
        FileSystem.Directory.GetCurrentDirectory().Returns(Directory);
        FileSystem.Path.Combine(Arg.Any<string>(), Arg.Any<string>()).Returns(FilePath);

        SerializerOptions = new();

        Repository = new JsonFileGameRepository(FileSystem, SerializerOptions);
    }

    protected string FilePath { get; }

    protected IFileSystem FileSystem { get; }

    protected IGameRepository Repository { get; }

    protected JsonSerializerOptions SerializerOptions { get; }
}
