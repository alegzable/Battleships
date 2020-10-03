using System;
using System.Collections.Generic;
using System.Linq;
using Battleships;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class ShipTest
    {
        [Test]
        public void CanBeShotAt_PositionMatchedAndHitsNotMatched_ReturnsTrue()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2),
                new Coordinates('A', 3),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");

            foreach (var shipCoordinate in shipCoordinates)
            {
                ship.CanBeShotAt(shipCoordinate).Should().BeTrue();
            }
        }
        
        [Test]
        public void CanBeShotAt_PositionMatchedAndHitsMatched_ReturnsFalse()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");
            var hitCoordinates = shipCoordinates[0];
            
            ship.TakeHit(hitCoordinates);

            ship.CanBeShotAt(hitCoordinates).Should().BeFalse();
        }
        
        [Test]
        public void CanBeShotAt_PositionNotMatched_ReturnsFalse()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");

            ship.CanBeShotAt(new Coordinates('A', 2)).Should().BeFalse();
        }
        
        [Test]
        public void TakeHit_PositionNotMatched_Throws()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");

            Action action = () => ship.TakeHit(new Coordinates('A', 2));

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void TakeHit_SameCoordinatesMultipleTimes_SingleHit()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");
            
            ship.TakeHit(shipCoordinates[0]);
            ship.TakeHit(shipCoordinates[0]);

            ship.Hits.Get.Count.Should().Be(1);
        }

        [Test]
        public void IsSunk_NotAllPositionsHit_ReturnsFalse()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");
            ship.TakeHit(shipCoordinates[0]);

            ship.IsSunk.Should().BeFalse();
        }
        
        [Test]
        public void IsSunk_AllPositionsHit_ReturnsTrue()
        {
            var shipCoordinates = new []
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2),
            };
            
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test");
            ship.TakeHit(shipCoordinates[0]);
            ship.TakeHit(shipCoordinates[1]);

            ship.IsSunk.Should().BeTrue();
        }
    }
}