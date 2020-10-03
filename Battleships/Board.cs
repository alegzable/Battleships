using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Battleships
{
    public class Board : IBoard
    { 
        public ImmutableList<Ship> Ships { get; }
        
        public Board(IList<Ship> ships)
        {
            Ships = ships.ToImmutableList();
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