using System.Collections.Generic;
using System.Linq;
using Battleships;
using Moq;
using NUnit.Framework;

namespace BattleshipsTests
{
    public class GameTests
    {
        [Test]
        public void Play_AllShipsSunk_StopsRunning()
        {
            var ship1Coordinates = new[]
            {
                new Coordinates('A', 1),
            };

            var ship2Coordinates = new[]
            {
                new Coordinates('B', 1),
            };
            
            var boardGeneratorMock = new Mock<IBoardGenerator>();

            boardGeneratorMock.Setup(x => x.Generate())
                .Returns(new Board(
                    new List<Ship>
                    {
                        new Ship(new HashSet<Coordinates>(ship1Coordinates), "Test"),
                        new Ship(new HashSet<Coordinates>(ship2Coordinates), "Test")
                    }, new SortedSet<Coordinates>(ship1Coordinates.Concat(ship2Coordinates))
                ));

            var fleetCommanderMock = new Mock<IFleetCommander>();
            fleetCommanderMock.SetupSequence(x => x.ProvideCoordinates())
                .Returns(() => new Maybe<Coordinates>(ship1Coordinates[0]))
                .Returns(() => new Maybe<Coordinates>(ship2Coordinates[0]));

            var game = new Game(boardGeneratorMock.Object, fleetCommanderMock.Object, new Mock<ICommentator>().Object);

            game.Play();

            Assert.Pass("The game has stopped running");
        }
        
        [Test]
        public void Play_BoardReceivesAllHitsByFleetCommander()
        {
            var shipCoordinates = new[]
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2)
            };
            var ship = new Ship(new HashSet<Coordinates>(shipCoordinates), "Test"); 
            var boardGeneratorMock = new Mock<IBoardGenerator>();
            var boardMock = new Mock<IBoard>();
            
            boardMock.SetupSequence(x => x.ReceiveShot(It.Is<Coordinates>(c => shipCoordinates.Contains(c))))
                .Returns(new ShipShotResult(ShotResult.Hit, new Maybe<Ship>(ship)))
                .Returns(new ShipShotResult(ShotResult.Won, new Maybe<Ship>(ship)));
            
            boardMock.Setup(x => x.ReceiveShot(It.Is<Coordinates>(c => !shipCoordinates.Contains(c))))
                .Returns(new ShipShotResult(ShotResult.Missed, new Maybe<Ship>()));
            
            boardMock.Setup(x => x.AllCoordinates)
                .Returns(new SortedSet<Coordinates>(shipCoordinates));
            
            boardGeneratorMock.Setup(x => x.Generate())
                .Returns(boardMock.Object);

            var fleetCommanderMock = new Mock<IFleetCommander>();
            fleetCommanderMock.SetupSequence(x => x.ProvideCoordinates())
                .Returns(() => new Maybe<Coordinates>(shipCoordinates[0]))
                .Returns(() => new Maybe<Coordinates>(new Coordinates('A', 3)))
                .Returns(() => new Maybe<Coordinates>(new Coordinates('B', 1)))
                .Returns(() => new Maybe<Coordinates>(new Coordinates('C', 1)))
                .Returns(() => new Maybe<Coordinates>(shipCoordinates[1]));

            var game = new Game(boardGeneratorMock.Object, fleetCommanderMock.Object, new Mock<ICommentator>().Object);

            game.Play();
            
            boardMock.Verify(x=>x.ReceiveShot(shipCoordinates[0]), Times.Once());
            boardMock.Verify(x=>x.ReceiveShot(shipCoordinates[1]), Times.Once());
            boardMock.Verify(x=>x.ReceiveShot(It.IsAny<Coordinates>()), Times.Exactly(5));
        }
    }
}