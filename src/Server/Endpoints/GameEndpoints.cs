using Application.Commands.JoinGame;
using Application.Queries.GetBoard;
using MediatR;

namespace Server.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/join", JoinGame).WithName("JoinGame").WithOpenApi();
        endpoints.MapGet("/game/{gameId}", GetGameState);
    }

    public static async Task<IResult> JoinGame(
        HttpContext httpContext,
        IMediator mediator)
    {
        var gameId = Guid.NewGuid();
        var playerId = GetPlayerId(httpContext);
        var result = await mediator.Send(new JoinGameCommand(gameId, false, playerId));

        if (!result.IsSuccess)
        {
            return TypedResults.Problem();
        }

        return TypedResults.Ok(gameId);
    }

    private static async Task<IResult> GetGameState(
        HttpContext httpContext,
        IMediator mediator,
        Guid gameId)
    {
        var playerId = GetPlayerId(httpContext);
        var gameState = await mediator.Send(new GetGameStateQuery(gameId, playerId));

        return TypedResults.Ok(gameState);
    }

    private static Guid GetPlayerId(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("x-player-id", out var playerIdHeader))
        {
            if (Guid.TryParse(playerIdHeader, out var playerId))
            {
                return playerId;
            }
        }

        return Guid.Empty;
    }
}
