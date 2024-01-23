using Application.Models;

namespace Application.Queries.GetBoard;
public readonly record struct GetGameStateQuery(Guid GameId, Guid PlayerId) : IRequest<IResult<GameStatePlayerView>>;
