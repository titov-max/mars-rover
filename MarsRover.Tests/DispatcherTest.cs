using System;
using Xunit;
using MarsRover;
using MarsRover.Exceptions;

namespace MarsRover.Tests
{
    public class DispatcherTest
    {
        private Dispatcher dispatcher;

        public DispatcherTest() {
            dispatcher = new Dispatcher(5, 5);
        }
        
        [Fact]
        public void ShouldAppropriatelyMoveFirstRover()
        {
            dispatcher.AddRover(1, 2, 'N');
            
            var result = dispatcher.ExecuteQueue(0, "LMLMLMLMM");

            Assert.Equal("1 3 N", result);
        }

        [Fact]
        public void ShouldAppropriatelyMoveSecondRover()
        {
            dispatcher.AddRover(3, 3, 'E');
            
            var result = dispatcher.ExecuteQueue(0, "MMRMMRMRRM");

            Assert.Equal("5 1 E", result);
        }

        [Fact]
        public void ShouldMoveRightByOnePoint()
        {
            dispatcher.AddRover(3, 3, 'E');
            
            var result = dispatcher.SendCommand(0, 'M');

            Assert.Equal("4 3 E", result);
        }

        [Fact]
        public void ShouldMoveLeftByOnePoint()
        {
            dispatcher.AddRover(3, 3, 'W');
            
            var result = dispatcher.SendCommand(0, 'M');

            Assert.Equal("2 3 W", result);
        }

        [Fact]
        public void ShouldMoveUpByOnePoint()
        {
            dispatcher.AddRover(3, 3, 'N');
            
            var result = dispatcher.SendCommand(0, 'M');

            Assert.Equal("3 4 N", result);
        }

        [Fact]
        public void ShouldMoveDownByOnePoint()
        {
            dispatcher.AddRover(3, 3, 'S');
            
            var result = dispatcher.SendCommand(0, 'M');

            Assert.Equal("3 2 S", result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenIncorrectBearingPassed()
        {
            Assert.Throws<UnknownBearingException>(() => dispatcher.AddRover(0, 0, 'X'));
        }

        [Fact]
        public void ShouldThrowExceptionWhenCoordinatesOutOfGrid()
        {
            Assert.Throws<OutOfGridException>(() => dispatcher.AddRover(6, 6, 'E'));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPlaceIsOccuppied()
        {
            dispatcher.AddRover(3, 3, 'N');
            Assert.Throws<GridPointOccupiedException>(() => dispatcher.AddRover(3, 3, 'S'));
        }

        [Fact]
        public void ShouldThrowExceptionWhenIncorrectCommandPassed()
        {
            dispatcher.AddRover(0, 0, 'N');
            Assert.Throws<UnknownCommandException>(() => dispatcher.SendCommand(0, 'X'));
        }

        [Fact]
        public void ShouldKeepLocationWhenPointIsOccupiedByAnotherRover()
        {
            dispatcher.AddRover(3, 3, 'N');
            dispatcher.AddRover(2, 3, 'E');

            var result = dispatcher.SendCommand(1, 'M');

            Assert.Equal("2 3 E", result);
        }

        [Theory]
        [InlineData("M")]
        [InlineData("LM")]
        public void ShouldKeepLocationWhenMoveOutOfGrid(string commandQueue) {
            dispatcher.AddRover(0, 0, 'W');

            var result = dispatcher.ExecuteQueue(0, commandQueue);

            Assert.Equal("0 0", result.Substring(0, 3));
        }
    }
}
