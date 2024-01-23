using Application.Models;
using Domain.Aggregates.Game;

namespace Application.Mappers;
public class GameToGameStatePlayerViewMapper
{
    public GameStatePlayerView Map(Game game, Guid playerId)
    {
        var cells = new CellState[game.Width, game.Height];

        for (int y = 0; y < game.Height; y++)
        {
            for (int x = 0; x < game.Width; x++)
            {
                cells[x, y] = CellState.Empty;
            }
        }

        var isCurrentPlayer = game.GetIsCurrentPlayer(playerId);

        return new GameStatePlayerView(cells, isCurrentPlayer);
    }
}
