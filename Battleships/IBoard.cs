using System.Collections.Generic;

namespace Battleships
{
    public interface IBoard
    {
        List<Ship> Ships { get; }
        ShipShotResult ReceiveShot(Coordinates coordinates);
    }
}