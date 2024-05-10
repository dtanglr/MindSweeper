using SchneiderElectric.MindSweeper.Domain.Repositories;

namespace SchneiderElectric.MindSweeper.Persistence.Repositories;

public class JsonFileGameRepository : IGameRepository
{
    public Task<Game> GetGameAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveGameAsync(Game game)
    {
        throw new NotImplementedException();
    }
}
