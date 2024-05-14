using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public record MoveCommandResponse(
    Game Game,
    string FromSquare,
    string ToSquare,
    Direction Direction,
    bool HitBomb);
