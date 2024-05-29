namespace MindSweeper.Domain;

public record PlayerContext(string Id)
{
    public Game? Game { get; internal set; }

    public bool HasGame => Game is not null;
}
