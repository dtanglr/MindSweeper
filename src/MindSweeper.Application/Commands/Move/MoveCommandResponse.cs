using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Represents the response of a move command.
/// </summary>
public record MoveCommandResponse(
    Game Game,
    string FromSquare,
    string ToSquare,
    Direction Direction,
    bool HitBomb);
