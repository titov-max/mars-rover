using System;
using Xunit;
using MarsRover;

namespace MarsRover.Tests
{
    public class RoverTest
    {
        private Rover rover;

        public RoverTest() {
            rover = new Rover(new Position(3, 3, 'N'));
        }
        
        [Fact]
        public void ShouldBeTurnedToWest()
        {
            rover.TurnLeft();

            var newPosition = rover.Current;

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.West, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeTurnedToEst()
        {
            rover.TurnRight();

            var newPosition = rover.Current;

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.East, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeTurnedRound()
        {
            for (var i = 0; i < 4; i++) {
                rover.TurnRight();
            }

            var newPosition = rover.Current;

            Assert.Equal(3, newPosition.X);
            Assert.Equal(3, newPosition.Y);
            Assert.Equal(Bearings.North, newPosition.Bearing);
        }

        [Fact]
        public void ShouldBeMovedToNorth()
        {
            rover.Move();

            var newPosition = rover.Current;

            Assert.Equal(3, newPosition.X);
            Assert.Equal(4, newPosition.Y);
            Assert.Equal(Bearings.North, newPosition.Bearing);
        }
    }
}
