namespace SchneiderElectric.MindSweeper.Domain;

public record Game(
    Guid Id,
    string PlayerId,
    Settings Settings,
    List<int> Bombs,
    int Lives,
    int Moves,
    string CurrentSquare,
    Dictionary<Direction, string> AvailableMoves)
{
    public GameStatus Status => this switch
    {
        { Lives: 0 } => GameStatus.Lost,
        _ when !AvailableMoves.ContainsKey(Direction.Up) => GameStatus.Won,
        _ => GameStatus.InProgress
    };
}
