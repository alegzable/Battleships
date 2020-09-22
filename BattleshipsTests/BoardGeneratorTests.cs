using Battleships;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class BoardGeneratorTests
    {
        [Test]
        public void Generate_ShipsDontOverlap()
        {
            var boardGenerator = new BoardGenerator();
            
            var board = boardGenerator.Generate();
            var coordinates = board.Ships
                .SelectMany(ship => ship.Position)
                .ToList();
            
            var uniqueCoordinates = new HashSet<Coordinates>(coordinates);

            uniqueCoordinates.Count.Should().Be(coordinates.Count);
        }
        
        [Test]
        public void Generate_CreatesBoardWithCorrectShips()
        {
            var boardGenerator = new BoardGenerator();
            
            var board = boardGenerator.Generate();

            board.Ships.Count.Should().Be(3);
            
            board.Ships
                .Count(ship => ship.Name == "Battleship" && ship.Position.Count == 5)
                .Should()
                .Be(1);
            
            board.Ships
                .Count(ship => ship.Name == "Destroyer" && ship.Position.Count == 4)
                .Should()
                .Be(2);
        }
    }
}