using Application.Commands.JoinGame;
using Application.Queries.GetBoard;
using MediatR;

namespace Server.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/join", JoinGame);
        endpoints.MapGet("/game/{gameId}/board", GetBoard);
    }

    public static async Task<IResult> JoinGame(IMediator mediator)
    {
        var gameId = Guid.NewGuid();
        var playerId = Guid.Empty; // TODO
        var result = await mediator.Send(new JoinGameCommand(gameId, false, playerId));

        if (!result.IsSuccess)
        {
            return TypedResults.Problem();
        }

        return TypedResults.Ok(gameId);
    }

    private static async Task GetBoard(
        Guid gameId,
        IMediator mediator)
    {
        var playerId = Guid.Empty; // TODO
        var game = await mediator.Send(new GetBoardQuery(gameId, playerId));
    }
}
