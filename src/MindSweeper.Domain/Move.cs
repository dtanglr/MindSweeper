namespace MindSweeper.Domain;

/// <summary>
/// Represents a move made in the game.
/// </summary>
public record Move(Direction Direction, string FromSquare, string ToSquare, bool HitBomb);
