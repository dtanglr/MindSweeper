using MindSweeper.Domain;

namespace MindSweeper.Application;

public class GameOptions
{
    public Type? RepositoryType { get; private set; }

    public void UseRepository<TRepository>() where TRepository : class, IGameRepository
    {
        RepositoryType = typeof(TRepository);
    }
}
