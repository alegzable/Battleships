namespace Battleships
{
    public class ShipShotResult
    {
        public ShotResult Result { get; }
        public Maybe<Ship> MaybeShip { get; }

        public ShipShotResult(ShotResult result, Maybe<Ship> maybeShip)
        {
            Result = result;
            MaybeShip = maybeShip;
        }
    }
}