using System.Collections.Immutable;
using Domain.Aggregates.Game.Enums;
using Domain.ValueTypes;

namespace Domain.Aggregates.Game.Entities;
public sealed class Ship
{
    private static readonly ImmutableDictionary<ShipType, int> _lengths = new Dictionary<ShipType, int>
    {
        [ShipType.Carrier] = 5,
        [ShipType.Battleship] = 4,
        [ShipType.Destroyer] = 3,
        [ShipType.Submarine] = 3,
        [ShipType.PatrolBoat] = 2
    }
    .ToImmutableDictionary();

    public Ship(
        ShipType shipType,
        Coordinate location,
        ShipOrientation orientation)
    {
        if (!Enum.IsDefined(typeof(ShipType), shipType))
        {
            throw new ArgumentOutOfRangeException(nameof(shipType));
        }

        ShipType = shipType;
        Location = location;

        if (!Enum.IsDefined(typeof(ShipOrientation), orientation))
        {
            throw new ArgumentOutOfRangeException(nameof(orientation));
        }

        Orientation = orientation;
    }

    public static IReadOnlyDictionary<ShipType, int> Lengths => _lengths;

    public ShipType ShipType { get; }
    public Coordinate Location { get; }
    public ShipOrientation Orientation { get; }

    public int Length => Lengths[ShipType];

    public static int GetLength(ShipType shipType) => _lengths[shipType];

    public IEnumerable<Coordinate> EnumerateCoordinates()
    {
        for (int i = 0; i < Length; i++)
        {
            var coordinate = Orientation switch
            {
                ShipOrientation.Horizontal => new Coordinate(Location.X + i, Location.Y),
                ShipOrientation.Vertical => new Coordinate(Location.X, Location.Y + i),
                _ => throw new InvalidOperationException()
            };

            yield return coordinate;
        }
    }
}
