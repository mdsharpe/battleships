using Application.Mappers;
using Application.Models;
using Domain;

namespace Application.Queries.GetBoard;
internal class GetGameStateQueryHandler : IRequestHandler<GetGameStateQuery, IResult<GameStatePlayerView>>
{
    private readonly IGameRepository _gameRepository;
    private readonly GameToGameStatePlayerViewMapper _mapper;

    public GetGameStateQueryHandler(
        IGameRepository gameRepository,
        GameToGameStatePlayerViewMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<IResult<GameStatePlayerView>> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGame(request.GameId, cancellationToken);

        if (game is null)
        {
            return Result.Fail<GameStatePlayerView>("Game not found");
        }

        if (!game.GetHasPlayer(request.PlayerId))
        {
            return Result.Fail<GameStatePlayerView>("Player not in game");
        }

        var view = _mapper.Map(game, request.PlayerId);

        return Result.Ok(view);
    }
}
