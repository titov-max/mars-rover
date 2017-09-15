using System;
using System.Collections.Generic;
using MarsRover.Exceptions;

namespace MarsRover
{
    public class Dispatcher : IDispatcher
    {
        private int gridX;
        private int gridY;
        private bool[,] grid;
        private List<Rover> rovers = new List<Rover>();
        private Dictionary<char, Bearings> bearingsMap = new Dictionary<char, Bearings>()
        {
            { 'N', Bearings.North },
            { 'E', Bearings.East },
            { 'S', Bearings.South },
            { 'W', Bearings.West }
        };
        private Dictionary<char, Commands> commandsMap = new Dictionary<char, Commands>()
        {
            { 'L', Commands.Left },
            { 'R', Commands.Right },
            { 'M', Commands.Move }
        };

        public Dispatcher(int gridX, int gridY)
        {
            this.gridX = gridX;
            this.gridY = gridY;
            this.grid = new bool[gridX + 1, gridY + 1];
        }

        public void AddRover(int x, int y, char bearingSignal)
        {
            if (x > gridX || y > gridY)
            {
                throw new OutOfGridException($"Coordinates ({x},{y}) out of grid ({gridX},{gridY})");
            }

            if (!bearingsMap.ContainsKey(bearingSignal))
            {
                throw new UnknownBearingException($"Unknown bearing signal {bearingSignal}");
            }

            if (grid[x, y])
            {
                throw new GridPointOccupiedException($"Coordinates ({x},{y}) had been already occupied by another rover");
            }

            rovers.Add(
                new Rover(
                    new Location(x, y),
                    bearingsMap[bearingSignal]
                )
            );

            grid[x, y] = true;
        }

        public string ExecuteQueue(int roverIndex, string commandQueue) {
            foreach (var command in commandQueue)
            {
                SendCommand(roverIndex, command);
            }
            return GetRoverState(roverIndex);
        }

        public string SendCommand(int roverIndex, char commandSignal)
        {
            if (!commandsMap.ContainsKey(commandSignal))
            {
                throw new UnknownCommandException($"Unknown command signal {commandSignal}");
            }

            var rover = rovers[roverIndex];
            var command = commandsMap[commandSignal];
            switch (command)
            {
                case Commands.Left:
                    rover.TurnLeft();
                    break;
                case Commands.Right:
                    rover.TurnRight();
                    break;
                case Commands.Move:
                    if (rover.PredictedLocation.BelongsGrid(gridX, gridY) &&
                        !grid[rover.PredictedLocation.X, rover.PredictedLocation.Y]
                    )
                    {
                        grid[rover.CurrentLocation.X, rover.CurrentLocation.Y] = false;
                        rover.Move();
                        grid[rover.CurrentLocation.X, rover.CurrentLocation.Y] = true;
                    }
                    break;
            }

            return GetRoverState(roverIndex);
        }

        public string GetRoverState(int roverIndex) {
            return rovers[roverIndex].State;
        }
    }
}
