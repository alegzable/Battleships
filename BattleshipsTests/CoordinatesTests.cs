﻿using Battleships;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class CoordinatesTests
    {
        [Test]
        public void ToString_ReturnsColumnAndRow()
        {
            var coordinates = new Coordinates('A', 1);

            var result = coordinates.ToString();

            result.Should().Be("A1");
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            var coordinates = new Coordinates('A', 1);
            
            var result = coordinates.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            var coordinates = new Coordinates('A', 1);
            
            var result = coordinates.Equals(coordinates);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_SameColumnDifferentRow_ReturnsFalse()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('A', 2);
            
            var result = coordinates1.Equals(coordinates2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_SameRowDifferentColumn_ReturnsFalse()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('B', 1);
            
            var result = coordinates1.Equals(coordinates2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_SameRowAmdColumn_ReturnsTrue()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('A', 1);
            
            var result = coordinates1.Equals(coordinates2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ReturnsFalse()
        {
            var coordinates = new Coordinates('A', 1);
            
            var result = coordinates.Equals((object)null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_SameReference_ReturnsTrue()
        {
            var coordinates = new Coordinates('A', 1);
            
            var result = coordinates.Equals((object)coordinates);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_SameColumnDifferentRow_ReturnsFalse()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('A', 2);
            
            var result = coordinates1.Equals((object)coordinates2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_SameRowDifferentColumn_ReturnsFalse()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('B', 1);
            
            var result = coordinates1.Equals((object)coordinates2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_SameRowAmdColumn_ReturnsTrue()
        {
            var coordinates1 = new Coordinates('A', 1);
            var coordinates2 = new Coordinates('A', 1);
            
            var result = coordinates1.Equals((object)coordinates2);

            result.Should().BeTrue();
        }
    }
}