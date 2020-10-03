using System.Collections.Generic;

namespace Battleships
{
    public class ReadonlyCoordinatesSet
    {
        protected readonly HashSet<Coordinates> Coordinates;
        
        public IEnumerable<Coordinates> Get => Coordinates;
        public ReadonlyCoordinatesSet(HashSet<Coordinates> coordinates = null)
        {
            Coordinates = coordinates ?? new HashSet<Coordinates>();
        }

        public bool SetEquals(ReadonlyCoordinatesSet readonlyCoordinates)
        {
            return Coordinates.SetEquals(readonlyCoordinates.Coordinates);
        }

        public bool Contains(Coordinates coordinates)
        {
            return Coordinates.Contains(coordinates);
        }
    }
}