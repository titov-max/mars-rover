using System;
using Xunit;
using MarsRover;
using MarsRover.CustomExceptions;

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
            Assert.Throws<UnknownBearingException>(() => dispatcher.LaunchRover(rover, new Position(0, 0, 'X')));
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesOutOfGrid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => dispatcher.LaunchRover(rover, new Position(6, 6, 'E')));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPlaceIsOccuppied()
        {
            dispatcher.LaunchRover(rover, new Position(3, 3, 'N'));
            Assert.Throws<DispatcherException>(() => dispatcher.LaunchRover(new Rover(), new Position(3, 3, 'S')));
        }

        [Fact]
        public void ShouldThrowExceptionWhenIncorrectCommandPassed()
        {
            dispatcher.LaunchRover(rover, new Position(0, 0, 'N'));
            Assert.Throws<UnknownCommandException>(() => dispatcher.SendCommand(rover, 'X'));
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
