using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Battleships
{
    public class Ship
    {
        public string Name { get; }
        public ImmutableHashSet<Coordinates> Position { get; }
        public ImmutableHashSet<Coordinates> Hits { get; private set; } = ImmutableHashSet<Coordinates>.Empty;
        public bool IsSunk => Position.SetEquals(Hits);

        public Ship(HashSet<Coordinates> position, string name)
        {
            Position = position.ToImmutableHashSet();
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

            if (!Hits.Contains(coordinates))
            {
                Hits = Hits.Add(coordinates);
            }
        }
    }
}