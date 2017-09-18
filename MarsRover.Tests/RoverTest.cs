using System;
using Xunit;
using MarsRover;

namespace MarsRover.Tests
{
    public class RoverTest
    {
        private Rover rover;
        private Position position;

        public RoverTest() {
            rover = new Rover();
            position = new Position(3, 3, 'N');
        }
        
        [Fact]
        public void ShouldBeTurnedToWest()
        {
            var newPosition = rover.TurnLeft(position);

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.West, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeTurnedToEst()
        {
            var newPosition = rover.TurnRight(position);

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.East, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeTurnedRound()
        {
            Position newPosition = new Position(position.X, position.Y, position.Bearing);
            for (var i = 0; i < 4; i++) {
                newPosition = rover.TurnRight(newPosition);
            }

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.North, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeMovedToNorth()
        {
            var newPosition = rover.Move(position);

            Assert.Equal(3, newPosition.X);
            Assert.Equal(4, newPosition.Y);
            Assert.Equal(Bearings.North, newPosition.Bearing);
        }
    }
}
