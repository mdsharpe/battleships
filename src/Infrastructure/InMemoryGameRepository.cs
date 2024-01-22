using Domain;
using Domain.Aggregates.Game;

namespace Infrastructure;

public class InMemoryGameRepository : IGameRepository
{
    public Task<Game?> GetGame(Guid gameId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetGame(Guid gameId, Game game, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteGame(Guid gameId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
