using Domain;
using Domain.Exceptions;

namespace Application.Commands.JoinGame;
internal class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Result>
{
    private readonly IGameRepository _gameRepository;
    private readonly GameFactory _gameFactory;

    public JoinGameCommandHandler(
        IGameRepository gameRepository,
        GameFactory gameFactory)
    {
        _gameRepository = gameRepository;
        _gameFactory = gameFactory;
    }

    public async Task<Result> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGame(request.GameId, cancellationToken);

        if (game is null)
        {
            if (!request.CreateIfNotExists)
            {
                return Result.Fail("Game not found.");
            }

            game = _gameFactory.CreateGame(request.GameId);
        }

        try
        {
            game.JoinGame(request.PlayerId);
        }
        catch (DomainException ex)
        {
            return Result.Fail(ex.Message);
        }

        return Result.Ok();
    }
}
