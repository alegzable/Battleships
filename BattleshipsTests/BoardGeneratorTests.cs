using Battleships;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class BoardGeneratorTests
    {
        private readonly char[] _validColumns = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
        private readonly int[] _validRows = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        
        [Test]
        public void Generate_ShipsDontOverlap()
        {
            var boardGenerator = new BoardGenerator(_validColumns, _validRows);
            
            var board = boardGenerator.Generate();
            var coordinates = board.Ships
                .SelectMany(ship => ship.Position.Get)
                .ToList();
            
            var uniqueCoordinates = new HashSet<Coordinates>(coordinates);

            uniqueCoordinates.Count.Should().Be(coordinates.Count);
        }
        
        [Test]
        public void Generate_CreatesBoardWithCorrectShips()
        {
            var boardGenerator = new BoardGenerator(_validColumns, _validRows);
            
            var board = boardGenerator.Generate();

            board.Ships.Count.Should().Be(3);
            
            board.Ships
                .Count(ship => ship.Name == "Battleship" && ship.Position.Get.Count() == 5)
                .Should()
                .Be(1);
            
            board.Ships
                .Count(ship => ship.Name == "Destroyer" && ship.Position.Get.Count() == 4)
                .Should()
                .Be(2);
        }
    }
}