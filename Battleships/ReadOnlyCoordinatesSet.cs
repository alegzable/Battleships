using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class ReadOnlyCoordinatesSet
    {
        protected readonly HashSet<Coordinates> Coordinates;
        
        public IReadOnlyCollection<Coordinates> Get => Coordinates.ToList().AsReadOnly();
        public ReadOnlyCoordinatesSet(HashSet<Coordinates> coordinates = null)
        {
            Coordinates = coordinates ?? new HashSet<Coordinates>();
        }

        public bool SetEquals(ReadOnlyCoordinatesSet readOnlyCoordinates)
        {
            return Coordinates.SetEquals(readOnlyCoordinates.Coordinates);
        }

        public bool Contains(Coordinates coordinates)
        {
            return Coordinates.Contains(coordinates);
        }
    }
}