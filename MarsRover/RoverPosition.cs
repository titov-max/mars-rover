using System;

namespace MarsRover
{
    public class RoverPosition
    {
        public RoverPosition(Rover rover, Position position)
        {
            this.Rover = rover;
            this.Current = position;
        }

        public Rover Rover { get; set; }

        public Position Current { get; set; }

        public string State => $"{Current.X} {Current.Y} {Current.Bearing.ToString()[0]}";
    }
}
