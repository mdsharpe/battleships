using Application.Mappers;
using Application.Models;
using Domain;

namespace Application.Queries.GetBoard;
internal class GetBoardQueryHandler : IRequestHandler<GetBoardQuery, IResult<BoardPlayerView>>
{
    private readonly IGameRepository _gameRepository;
    private readonly GameToBoardPlayerViewMapper _mapper;

    public GetBoardQueryHandler(
        IGameRepository gameRepository,
        GameToBoardPlayerViewMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<IResult<BoardPlayerView>> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGame(request.GameId, cancellationToken);

        if (game is null)
        {
            return Result.Fail<BoardPlayerView>("Game not found");
        }

        if (!game.GetHasPlayer(request.PlayerId))
        {
            return Result.Fail<BoardPlayerView>("Player not in game");
        }

        var view = _mapper.Map(game, request.PlayerId);

        return Result.Ok(view);
    }
}
