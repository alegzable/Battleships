using System.Collections.Immutable;

namespace Battleships
{
    public interface IBoard
    {
        ImmutableList<Ship> Ships { get; }
        ShipShotResult ReceiveShot(Coordinates coordinates);
    }
}