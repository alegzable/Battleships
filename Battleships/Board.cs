using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Board : IBoard
    { 
        public List<Ship> Ships { get; }
        public SortedSet<Coordinates> AllCoordinates { get; }
        public Board(List<Ship> ships, SortedSet<Coordinates> allCoordinates)
        {
            Ships = ships;
            AllCoordinates = allCoordinates;
        }

        public ShipShotResult ReceiveShot(Coordinates coordinates)
        {
            var shipToHit = Ships.SingleOrDefault(ship => ship.CanBeShotAt(coordinates));
            if (shipToHit == null)
            {
                return new ShipShotResult(ShotResult.Missed, new Maybe<Ship>());
            }
            
            shipToHit.TakeHit(coordinates);

            if (Ships.All(ship => ship.IsSunk))
            {
                return new ShipShotResult(ShotResult.Won, new Maybe<Ship>(shipToHit));
            }

            return shipToHit.IsSunk
                ? new ShipShotResult(ShotResult.Sunk, new Maybe<Ship>(shipToHit))
                : new ShipShotResult(ShotResult.Hit, new Maybe<Ship>(shipToHit));
        }
    }
}