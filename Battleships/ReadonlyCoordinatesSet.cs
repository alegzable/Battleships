using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class ReadonlyCoordinatesSet
    {
        protected readonly HashSet<Coordinates> Coordinates;
        
        public IReadOnlyCollection<Coordinates> Get => Coordinates.ToList().AsReadOnly();
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