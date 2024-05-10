namespace SchneiderElectric.MindSweeper.Domain.Repositories;

public interface IGameRepository
{
    Task<Game> GetGameAsync();
    Task SaveGameAsync(Game game);
}
