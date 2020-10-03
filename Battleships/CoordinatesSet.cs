namespace Battleships
{
    public class CoordinatesSet : ReadonlyCoordinatesSet
    {
        public void Add(Coordinates coordinates)
        {
            Coordinates.Add(coordinates);
        }
    }
}