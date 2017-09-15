using System;
using System.Collections.Generic;
using MarsRover.CustomExceptions;

namespace MarsRover
{
    public class Dispatcher
    {
        private int gridX;
        private int gridY;
        private Object moveLock = new Object();
        private readonly bool[,] grid;
        private readonly List<RoverPosition> roverPositions = new List<RoverPosition>();

        private readonly Dictionary<char, Commands> commandsMap = new Dictionary<char, Commands>()
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

        public void LaunchRover(Rover rover, Position position)
        {
            if (position.X > gridX || position.Y > gridY)
            {
                throw new ArgumentOutOfRangeException($"Coordinates ({position.X},{position.Y}) out of grid ({gridX},{gridY})");
            }

            if (grid[position.X, position.Y])
            {
                throw new DispatcherException($"Coordinates ({position.X},{position.Y}) had been already occupied by another rover");
            }

            roverPositions.Add(
                new RoverPosition(
                    rover,
                    position
                )
            );

            grid[position.X, position.Y] = true;
        }

        public bool IsGridPoint(Position position)
        {
            return 0 <= position.X && position.X <= gridX
                && 0 <= position.Y && position.Y <= gridY;
        }

        public string ExecuteQueue(Rover rover, string commandQueue)
        {
            foreach (var command in commandQueue)
            {
                SendCommand(rover, command);
            }
            return GetRoverState(rover);
        }

        public string SendCommand(Rover rover, char commandSignal)
        {
            if (!commandsMap.ContainsKey(commandSignal))
            {
                throw new UnknownCommandException($"Unknown command signal {commandSignal}");
            }

            var roverPosition = roverPositions.Find(rp => rp.Rover == rover);
            var command = commandsMap[commandSignal];
            switch (command)
            {
                case Commands.Left:
                    roverPosition.Current = rover.TurnLeft(roverPosition.Current);
                    break;
                case Commands.Right:
                    roverPosition.Current = rover.TurnRight(roverPosition.Current);
                    break;
                case Commands.Move:
                    lock (moveLock)
                    {
                        if (IsGridPoint(roverPosition.Next) &&
                            !grid[roverPosition.Next.X, roverPosition.Next.Y]
                        )
                        {
                            grid[roverPosition.Current.X, roverPosition.Current.Y] = false;
                            roverPosition.Current = rover.Move(roverPosition.Current);
                            grid[roverPosition.Current.X, roverPosition.Current.Y] = true;
                        }
                    }
                    break;
            }

            return GetRoverState(rover);
        }

        public string GetRoverState(Rover rover)
        {
            return roverPositions.Find(rp => rp.Rover == rover)?.State;
        }
    }
}
