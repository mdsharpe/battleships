using Domain.Aggregates.Game;
using Domain.Aggregates.Game.Entities;
using Domain.Aggregates.Game.Enums;
using Domain.ValueTypes;

namespace Application;
internal class GameFactory
{
    private const int PlayerCount = 2;
    private const int DefaultWidth = 10;
    private const int DefaultHeight = 10;
    private static readonly Random _rng = new();

    public Game CreateGame(Guid id)
        => new(
            id,
            DefaultWidth,
            DefaultHeight,
            CreateShips());

    private static IEnumerable<IEnumerable<Ship>> CreateShips()
    {
        for (int playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
        {
            var playerShips = new List<Ship>();

            foreach (var shipType in EnumerateShipTypes())
            {
                var length = Ship.GetLength(shipType);
                Ship ship;

                do
                {
                    var orientation = GetRandomOrientation();
                    var location = new Coordinate(
                        _rng.Next(DefaultWidth - (orientation == ShipOrientation.Horizontal ? length : 0)),
                        _rng.Next(DefaultHeight - (orientation == ShipOrientation.Vertical ? length : 0)));

                    ship = new Ship(shipType, location, orientation);
                } while (GetHasAnyCollision(playerShips, ship));

                playerShips.Add(ship);
            }

            yield return playerShips;
        }
    }

    private static bool GetHasAnyCollision(IEnumerable<Ship> existingShips, Ship newShip)
    {
        var occupiedCoords = existingShips
            .SelectMany(o => o.EnumerateCoordinates());

        var collisions = newShip
            .EnumerateCoordinates()
            .Intersect(occupiedCoords);

        return collisions.Any();
    }

    private static IEnumerable<ShipType> EnumerateShipTypes()
        => Enum.GetValues<ShipType>()
            .Except([ShipType.Unknown]);

    private static ShipOrientation GetRandomOrientation()
        => _rng.Next(2) switch
        {
            0 => ShipOrientation.Horizontal,
            1 => ShipOrientation.Vertical,
            _ => throw new InvalidOperationException()
        };
}
