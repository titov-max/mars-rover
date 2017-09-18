using System;
using Xunit;
using MarsRover;

namespace MarsRover.Tests
{
    public class DispatcherTest
    {
        private Dispatcher dispatcher;
        private Rover rover;

        public DispatcherTest() {
            dispatcher = new Dispatcher(5, 5);
            rover = new Rover();
        }
        
        [Fact]
        public void ShouldAppropriatelyMoveFirstRover()
        {
            var position = new Position(1, 2, 'N');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.ExecuteQueue(rover, "LMLMLMLMM");

            Assert.Equal("1 3 N", result);
        }

        [Fact]
        public void ShouldAppropriatelyMoveSecondRover()
        {
            var position = new Position(3, 3, 'E');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.ExecuteQueue(rover, "MMRMMRMRRM");

            Assert.Equal("5 1 E", result);
        }

        [Fact]
        public void ShouldMoveRightByOnePoint()
        {
            var position = new Position(3, 3, 'E');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.SendCommand(rover, 'M');

            Assert.Equal("4 3 E", result);
        }

        [Fact]
        public void ShouldMoveLeftByOnePoint()
        {
            var position = new Position(3, 3, 'W');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.SendCommand(rover, 'M');

            Assert.Equal("2 3 W", result);
        }

        [Fact]
        public void ShouldMoveUpByOnePoint()
        {
            var position = new Position(3, 3, 'N');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.SendCommand(rover, 'M');

            Assert.Equal("3 4 N", result);
        }

        [Fact]
        public void ShouldMoveDownByOnePoint()
        {
            var position = new Position(3, 3, 'S');
            dispatcher.LaunchRover(rover, position);
            
            var result = dispatcher.SendCommand(rover, 'M');

            Assert.Equal("3 2 S", result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenIncorrectBearingPassed()
        {
            Exception ex = Assert.Throws<Exception>(() => dispatcher.LaunchRover(rover, new Position(0, 0, 'X')));

            Assert.Equal("Unknown bearing X", ex.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesOutOfGrid()
        {
            Exception ex = Assert.Throws<Exception>(() => dispatcher.LaunchRover(rover, new Position(6, 6, 'E')));

            Assert.Equal("Coordinates (6, 6) out of grid (5, 5)", ex.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenPlaceIsOccuppied()
        {
            dispatcher.LaunchRover(rover, new Position(3, 3, 'N'));

            Exception ex = Assert.Throws<Exception>(() => dispatcher.LaunchRover(new Rover(), new Position(3, 3, 'S')));

            Assert.Equal("Coordinates (3, 3) is already occupied by another rover", ex.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenIncorrectCommandPassed()
        {
            dispatcher.LaunchRover(rover, new Position(0, 0, 'N'));

            Exception ex = Assert.Throws<Exception>(() => dispatcher.SendCommand(rover, 'X'));

            Assert.Equal("Unknown command X", ex.Message);
        }

        [Fact]
        public void ShouldKeepLocationWhenPointIsOccupiedByAnotherRover()
        {
            dispatcher.LaunchRover(new Rover(), new Position(3, 3, 'N'));
            dispatcher.LaunchRover(rover, new Position(2, 3, 'E'));

            var result = dispatcher.SendCommand(rover, 'M');

            Assert.Equal("2 3 E", result);
        }

        [Theory]
        [InlineData("M")]
        [InlineData("LM")]
        public void ShouldKeepLocationWhenMoveOutOfGrid(string commandQueue) {
            var position = new Position(0, 0, 'W');
            dispatcher.LaunchRover(rover, position);

            var result = dispatcher.ExecuteQueue(rover, commandQueue);

            Assert.Equal("0 0", result.Substring(0, 3));
        }
    }
}
