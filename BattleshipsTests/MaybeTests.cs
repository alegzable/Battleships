using Battleships;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class MaybeTests
    {
        [Test]
        public void Ctor_Empty_HasValueIsFalseAndValueIsNull()
        {
            var maybe = new Maybe<string>();

            maybe.HasValue.Should().BeFalse();
            maybe.Value.Should().BeNull();
        }
        
        [Test]
        public void Ctor_Null_HasValueIsFalseAndValueIsNull()
        {
            var maybe = new Maybe<string>(null);

            maybe.HasValue.Should().BeFalse();
            maybe.Value.Should().BeNull();
        }
        
        [Test]
        public void Ctor_NotNull_HasValueIsFalseAndValueIsNull()
        {
            var maybe = new Maybe<string>("");

            maybe.HasValue.Should().BeTrue();
            maybe.Value.Should().NotBeNull();
        }
    }
}