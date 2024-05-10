using static SchneiderElectric.MindSweeper.Field;

namespace SchneiderElectric.MindSweeper;

public record Game
{
    public Settings Settings { get; init; } = new Settings();

    public required List<int> Bombs { get; init; } = new Bombs();

    public required List<Direction> Moves { get; init; }

    public required string StartSquare { get; init; }
}
