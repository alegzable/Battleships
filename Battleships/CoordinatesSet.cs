namespace Battleships
{
    public class CoordinatesSet : ReadOnlyCoordinatesSet
    {
        public void Add(Coordinates coordinates)
        {
            Coordinates.Add(coordinates);
        }
    }
}