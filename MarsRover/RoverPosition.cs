using System;
using MarsRover.CustomExceptions;

namespace MarsRover
{
    public class RoverPosition
    {
        public RoverPosition(Rover rover, Position position)
        {
            this.Rover = rover;
            this.Current = position;
        }

        public Position Current { get; set; }
        public Rover Rover { get; set; }

        public Position Next
        {
            get
            {
                switch (Current.Bearing)
                {
                    case Bearings.North:
                        return new Position(Current.X, Current.Y + 1, Current.Bearing);
                    case Bearings.East:
                        return new Position(Current.X + 1, Current.Y, Current.Bearing);
                    case Bearings.South:
                        return new Position(Current.X, Current.Y - 1, Current.Bearing);
                    case Bearings.West:
                        return new Position(Current.X - 1, Current.Y, Current.Bearing);
                    default:
                        throw new UnknownBearingException($"Unknown bearing {Current.Bearing}");
                }
            }
        }

        public string State {
            get => $"{Current.X} {Current.Y} {Current.Bearing.ToString()[0]}";
        }
    }
}
