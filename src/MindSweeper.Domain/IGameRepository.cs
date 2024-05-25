namespace MindSweeper.Domain;

public interface IGameRepository
{
    Task<Result> CreateGameAsync(Game newGame, CancellationToken cancellationToken);
    Task<Result> DeleteGameAsync(string playerId, CancellationToken cancellationToken);
    Task<Result<Game>> GetGameAsync(string playerId, CancellationToken cancellationToken);
    Task<Result> UpdateGameAsync(Game updatedGame, CancellationToken cancellationToken);
}
