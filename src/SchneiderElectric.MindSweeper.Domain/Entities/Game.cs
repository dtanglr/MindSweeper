using SchneiderElectric.MindSweeper.Moves;

namespace SchneiderElectric.MindSweeper.Domain.Entities;

public record Game
{
    public required Settings Settings { get; init; }

    public required List<int> Bombs { get; init; }

    public required List<Move> Moves { get; init; }

    public required string StartSquare { get; init; }
}
