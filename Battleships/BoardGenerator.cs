using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class BoardGenerator : IBoardGenerator
    {
        private readonly char[] _validColumns = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
        private readonly int[] _validRows = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        private readonly List<Ship> _ships = new List<Ship>();
        private readonly Random _random;

        public BoardGenerator()
        {
            _random = new Random();
        }

        public IBoard Generate()
        {
            _ships.Add(GetShip("Battleship", 5));
            _ships.Add(GetShip("Destroyer", 4));
            _ships.Add(GetShip("Destroyer", 4));

            return new Board(_ships, GetAllCells());
        }

        private SortedSet<Coordinates> GetAllCells()
        {
            var coordinates = _validRows.SelectMany(row =>
                _validColumns.Select(column => new Coordinates(column, row)));

            return new SortedSet<Coordinates>(coordinates);
        }

        private Ship GetShip(string name, int length)
        {
            var maybeShip = new Maybe<Ship>();

            while (!maybeShip.HasValue)
            {
                maybeShip = TryGetShip(name, length);
            }

            return maybeShip.Value;
        }

        private Maybe<Ship> TryGetShip(string name, int length)
        {
            var orientation = GetRandom(0, 2) == 0
                ? Orientation.Horizontal
                : Orientation.Vertical;

            var startingCoordinates = GetRandomCoordinates();
            var maybeShip = TryGetShip(name, startingCoordinates, length, orientation);

            return maybeShip.HasValue
                ? maybeShip
                : TryGetShip(name, startingCoordinates, length, GetOtherOrientation(orientation));
        }

        private Maybe<Ship> TryGetShip(
            string name,
            Coordinates startingCoordinates,
            int length,
            Orientation orientation)
        {
            var shipCoordinates = new List<Coordinates>
            {
                startingCoordinates
            };

            for (var i = 0; i < length - 1; i++)
            {
                var maybeNextCoordinates = GetNextCoordinates(shipCoordinates[i], orientation);

                if (!maybeNextCoordinates.HasValue ||
                    _ships.Any(ship => ship.Position.Contains(maybeNextCoordinates.Value)))
                {
                    return new Maybe<Ship>();
                }

                shipCoordinates.Add(maybeNextCoordinates.Value);
            }

            return new Maybe<Ship>(new Ship(new HashSet<Coordinates>(shipCoordinates), name));
        }

        private Coordinates GetRandomCoordinates()
        {
            var randomColumnIndex = GetRandom(0, _validColumns.Length);
            var randomRowIndex = GetRandom(0, _validRows.Length);

            var coordinates = new Coordinates(_validColumns[randomColumnIndex], _validRows[randomRowIndex]);

            return _ships.Any(ship => ship.Position.Contains(coordinates))
                ? GetRandomCoordinates()
                : coordinates;
        }

        private Maybe<Coordinates> GetNextCoordinates(Coordinates coordinates, Orientation orientation)
        {
            return orientation == Orientation.Horizontal
                ? GetNextHorizontalCoordinates(coordinates)
                : GetNextVerticalCoordinates(coordinates);
        }

        private Maybe<Coordinates> GetNextVerticalCoordinates(Coordinates coordinates)
        {
            var nextRowIndex = Array.IndexOf(_validRows, coordinates.Row) + 1;
            var isNextRowAvailable = nextRowIndex < _validRows.Length;

            return isNextRowAvailable
                ? new Maybe<Coordinates>(
                    new Coordinates(coordinates.Column, _validRows[nextRowIndex]))
                : new Maybe<Coordinates>();
        }

        private Maybe<Coordinates> GetNextHorizontalCoordinates(Coordinates coordinates)
        {
            var nextColumnIndex = Array.IndexOf(_validColumns, coordinates.Column) + 1;
            var isNextColumnAvailable = nextColumnIndex < _validColumns.Length;

            return isNextColumnAvailable
                ? new Maybe<Coordinates>(
                    new Coordinates(_validColumns[nextColumnIndex], coordinates.Row))
                : new Maybe<Coordinates>();
        }

        private static Orientation GetOtherOrientation(Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
        }

        private int GetRandom(int minInclusive, int maxExclusive)
        {
            return _random.Next(minInclusive, maxExclusive);
        }

        private enum Orientation
        {
            Horizontal = 1,
            Vertical = 2
        }
    }
}