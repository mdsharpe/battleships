using Domain.Aggregates.Game;

namespace Domain;
public interface IGameRepository
{
    Task<Game?> GetGame(Guid gameId, CancellationToken cancellationToken);
    Task SetGame(Guid gameId, Game game, CancellationToken cancellationToken);
    Task DeleteGame(Guid gameId, CancellationToken cancellationToken);
}
