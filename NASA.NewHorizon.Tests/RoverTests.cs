using System;
using Moq;
using NASA.NewHorizon.PlutoRover;
using NUnit.Framework;

namespace NASA.NewHorizon.Tests
{
    [TestFixture]
    public class RoverTests
    {
        private Mock<IObstacleDetector> _obstacleDetectorMock;

        [SetUp]
        public void Setup()
        {
            _obstacleDetectorMock = new Mock<IObstacleDetector>();
        }
        [Test]
        public void CommandFMovesRoverOneNorthWhenFacingNorth()
        {
            
            var rover = new Rover(1, 1, Direction.NavigationPoints.North,_obstacleDetectorMock.Object);
            rover.Move("F");
            Assert.AreEqual(1, rover.Position.X);

            Assert.AreEqual(2, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.North,rover.NavigationPoint);
        }
        [Test]
        public void CommandFFRFFMovesRoverFrom0x0To2x2WhenFacingNorth()
        {
            var rover = new Rover(0, 0, Direction.NavigationPoints.North,_obstacleDetectorMock.Object);
            rover.Move("FFRFF");
            Assert.AreEqual(2, rover.Position.X);

            Assert.AreEqual(2, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.East,rover.NavigationPoint);

        }
        [Test]
        public void CommandBMovesRoverTo0x1FacingNorthWhenIsAt0x0FacingNorth()
        {

            var rover = new Rover(0, 0, Direction.NavigationPoints.North,_obstacleDetectorMock.Object);
            rover.Move("B");
            Assert.AreEqual(0, rover.Position.X);

            Assert.AreEqual(1, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.South,rover.NavigationPoint);

        }
        [Test]
        public void CommandFFBBMovesRoverBackToItsPosition()
        {
            var rover = new Rover(10, 9, Direction.NavigationPoints.South,_obstacleDetectorMock.Object);
            rover.Move("FFBB");
            Assert.AreEqual(10, rover.Position.X);

            Assert.AreEqual(9, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.South,rover.NavigationPoint);

        }
        
        [Test]
        public void CommandFFLLFFMovesRoverBack0x0FacingSouthWhenAt0x0FacingNorth()
        {
            var rover = new Rover(_obstacleDetectorMock.Object);
            rover.Move("FFLLFF");
            Assert.AreEqual(0, rover.Position.X);

            Assert.AreEqual(0, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.South,rover.NavigationPoint);

        }
        [Test]
        
        public void CommandFFLFFRFFFMovesRoverBack99x2FacingNorthAndThrowsObstacleExceptionWhenAt0x0FacingNorthAndThereIsAnObstacleAt99x3()
        {
            _obstacleDetectorMock.Setup(o => o.AnyObstacle(It.Is<Position>(p => p.X == 99 && p.Y == 3))).Returns(true);
            var rover = new Rover(_obstacleDetectorMock.Object);
            Assert.Throws<ObstacleException>(()=> rover.Move("FFLFFRFFF"));
            Assert.AreEqual(99, rover.Position.X);

            Assert.AreEqual(2, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.North,rover.NavigationPoint);
        }
        [Test]
        public void CommandBBMovesRoverFrom0x0To0x2FacingSouthWhenFacingNorth()
        {

            var rover = new Rover(_obstacleDetectorMock.Object);
            rover.Move("BB");
            Assert.AreEqual(0, rover.Position.X);

            Assert.AreEqual(2, rover.Position.Y);
            Assert.AreEqual(Direction.NavigationPoints.South,rover.NavigationPoint);

        }
    }
}