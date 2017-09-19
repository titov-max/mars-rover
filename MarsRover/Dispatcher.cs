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
        private readonly List<Rover> rovers = new List<Rover>();

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

        public Rover LaunchRover(Position position)
        {
            if (position.X > gridX || position.Y > gridY)
            {
                throw new Exception($"Coordinates ({position.X}, {position.Y}) out of grid ({gridX}, {gridY})");
            }

            if (grid[position.X, position.Y])
            {
                throw new Exception($"Coordinates ({position.X}, {position.Y}) is already occupied by another rover");
            }

            var rover = new Rover(position);
            rovers.Add(rover);

            grid[position.X, position.Y] = true;

            return rover;
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

            lock (moveLock)
            {
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

                        if (IsGridPoint(rover.Next) &&
                            !grid[rover.Next.X, rover.Next.Y]
                        )
                        {
                            grid[rover.Current.X, rover.Current.Y] = false;
                            rover.Move();
                            grid[rover.Current.X, rover.Current.Y] = true;
                        }

                        break;
                }
            }

            return rover.State;
        }
    }
}
