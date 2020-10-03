using System;
using System.Collections.Generic;

namespace Battleships
{
    public class Ship
    {
        public string Name { get; }
        public ReadOnlyCoordinatesSet Position { get; }
        public CoordinatesSet Hits { get; } = new CoordinatesSet();
        public bool IsSunk => Position.SetEquals(Hits);

        public Ship(HashSet<Coordinates> position, string name)
        {
            Position = new ReadOnlyCoordinatesSet(position);
            Name = name;
        }

        public bool CanBeShotAt(Coordinates coordinates)
        {
            return Position.Contains(coordinates) && !Hits.Contains(coordinates);
        }

        public void TakeHit(Coordinates coordinates)
        {
            if (!Position.Contains(coordinates))
            {
                throw new ArgumentOutOfRangeException($"Ship not found. Invalid {nameof(coordinates)}");
            }

            Hits.Add(coordinates);
        }
    }
}