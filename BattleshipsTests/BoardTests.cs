using System.Collections.Generic;
using System.Linq;
using Battleships;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class BoardTests
    {
        [Test]
        public void ReceiveShot_ShipMissed_ReturnsMiss()
        {
            var board = GetBoard();
            var emptyCoordinates = board.AllCoordinates
                .First(x => !board.Ships.SelectMany(ship => ship.Position.Get).Contains(x));

            var result = board.ReceiveShot(emptyCoordinates);

            result.Result.Should().Be(ShotResult.Missed);
            result.MaybeShip.HasValue.Should().BeFalse();
        }

        [Test]
        public void ReceiveShot_ShipHit_ReturnsHit()
        {
            var board = GetBoard();
            var hitCoordinates = board.Ships[0].Position.Get.First();

            var result = board.ReceiveShot(hitCoordinates);

            result.Result.Should().Be(ShotResult.Hit);
            result.MaybeShip.HasValue.Should().BeTrue();
            result.MaybeShip.Value.Should().NotBeNull();
        }

        [Test]
        public void ReceiveShot_AllPositionsOfSingleShipHit_ReturnsSunk()
        {
            var board = GetBoard();
            var shipCoordinates = board.Ships[0].Position.Get;
            ShipShotResult result = null;

            foreach (var coordinates in shipCoordinates)
            {
                result = board.ReceiveShot(coordinates);
            }

            result.Result.Should().Be(ShotResult.Sunk);
            result.MaybeShip.HasValue.Should().BeTrue();
            result.MaybeShip.Value.Should().NotBeNull();
        }

        [Test]
        public void ReceiveShot_AllShipsSunk_ReturnsWin()
        {
            var board = GetBoard();
            var shipCoordinates = board.Ships.SelectMany(ship => ship.Position.Get);
            ShipShotResult result = null;

            foreach (var coordinates in shipCoordinates)
            {
                result = board.ReceiveShot(coordinates);
            }
            
            result.Result.Should().Be(ShotResult.Won);
            result.MaybeShip.HasValue.Should().BeTrue();
            result.MaybeShip.Value.Should().NotBeNull();
        }

        private static Board GetBoard()
        {
            var ship1 = new Ship(new HashSet<Coordinates>(new[]
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2),
            }), "Test1");

            var ship2 = new Ship(new HashSet<Coordinates>(new[]
            {
                new Coordinates('B', 1),
                new Coordinates('B', 2),
            }), "Test2");

            var columns = new[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'};
            var rows = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            var coordinates = rows.SelectMany(row =>
                columns.Select(column => new Coordinates(column, row)));

            return new Board(new List<Ship> {ship1, ship2}, new SortedSet<Coordinates>(coordinates));
        }
    }
}