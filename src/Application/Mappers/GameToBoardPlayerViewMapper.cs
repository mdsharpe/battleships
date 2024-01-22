using Application.Models;
using Domain.Aggregates.Game;

namespace Application.Mappers;
public class GameToBoardPlayerViewMapper
{
    public BoardPlayerView Map(Game game, Guid playerId)
    {
        var cells = new CellState[game.Width, game.Height];

        for (int y = 0; y < game.Height; y++)
        {
            for (int x = 0; x < game.Width; x++)
            {
                cells[x, y] = CellState.Empty;
            }
        }

        return new BoardPlayerView(cells);
    }
}
