using System;
using System.Collections.Generic;

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
                throw new Exception($"Coordinates ({position.X}, {position.Y}) out of grid ({gridX}, {gridY})");
            }

            if (grid[position.X, position.Y])
            {
                throw new Exception($"Coordinates ({position.X}, {position.Y}) is already occupied by another rover");
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
            string state = "";
            foreach (var command in commandQueue)
            {
                state = SendCommand(rover, command);
            }
            return state;
        }

        public string SendCommand(Rover rover, char commandSignal)
        {
            if (!commandsMap.ContainsKey(commandSignal))
            {
                throw new Exception($"Unknown command {commandSignal}");
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
                        var next = roverPosition.Rover.Move(roverPosition.Current);
                        if (IsGridPoint(next) &&
                            !grid[next.X, next.Y]
                        )
                        {
                            grid[roverPosition.Current.X, roverPosition.Current.Y] = false;
                            roverPosition.Current = next;
                            grid[roverPosition.Current.X, roverPosition.Current.Y] = true;
                        }
                    }
                    break;
            }

            return roverPosition.State;
        }
    }
}
