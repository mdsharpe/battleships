namespace Application.Models;
public sealed record GameStatePlayerView(
    CellState [,] Grid,
    bool YourTurn);
