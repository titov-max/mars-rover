using System;
using Xunit;
using MarsRover;
using MarsRover.Exceptions;

namespace MarsRover.Tests
{
    public class RoverTest
    {
        private Rover rover;

        public RoverTest() {
            rover = new Rover(new Location(3, 3), Bearings.North);
        }
        
        [Fact]
        public void ShouldBeTurnedToWest()
        {
            rover.TurnLeft();

            Assert.Equal("3 3 W", rover.State);
        }

        [Fact]
        public void ShouldBeTurnedToEst()
        {
            rover.TurnRight();

            Assert.Equal("3 3 E", rover.State);
        }

        [Fact]
        public void ShouldBeTurnedRound()
        {
            rover.TurnRight();
            rover.TurnRight();
            rover.TurnRight();
            rover.TurnRight();

            Assert.Equal("3 3 N", rover.State);
        }

        [Fact]
        public void ShouldBeMovedToNorth()
        {
            rover.Move();

            Assert.Equal("3 4 N", rover.State);
        }
    }
}
