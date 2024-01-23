using System.Collections.Immutable;
using Domain.Aggregates.Game.Entities;
using Domain.Exceptions;

namespace Domain.Aggregates.Game;
public sealed class Game
{
    private readonly Ship[][] _ships;
    private readonly Guid[] _playerIds;

    public Game(
        Guid id,
        int width,
        int height,
        IEnumerable<IEnumerable<Ship>> ships)
    {
        Id = id;
        Width = width;
        Height = height;
        _ships = ships
            .Select(shipSet => shipSet.ToArray())
            .ToArray();
        _playerIds = ships.Select(_ => Guid.Empty).ToArray();
    }

    public Guid Id { get; }
    public int Width { get; }
    public int Height { get; }
    public int CurrentPlayerIndex { get; private set; }

    public void JoinGame(Guid playerId)
    {
        if (playerId == Guid.Empty)
        {
            throw new DomainException("Empty GUID is not a valid player ID.");
        }

        for (int i = 0; i < _playerIds.Length; i++)
        {
            if (_playerIds[i] == Guid.Empty)
            {
                _playerIds[i] = playerId;
                return;
            }
        }

        throw new DomainException("Game is full.");
    }

    public bool GetHasPlayer(Guid playerId)
        => _playerIds.Contains(playerId);

    public bool GetIsCurrentPlayer(Guid playerId)
        => GetHasPlayer(playerId) && _playerIds[CurrentPlayerIndex] == playerId;
}
