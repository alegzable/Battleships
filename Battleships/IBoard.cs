using System.Collections.Generic;

namespace Battleships
{
    public interface IBoard
    {
        List<Ship> Ships { get; }
        SortedSet<Coordinates> AllCoordinates { get; }
        ShipShotResult ReceiveShot(Coordinates coordinates);
    }
}