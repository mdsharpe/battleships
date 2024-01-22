using Application.Models;

namespace Application.Queries.GetBoard;
public readonly record struct GetBoardQuery(Guid GameId, Guid PlayerId) : IRequest<IResult<BoardPlayerView>>;
