using System;
using MarsRover.Exceptions;

namespace MarsRover
{
    public class Rover : IRover
    {
        private Location location;
        private Bearings bearing;
        public Rover(Location startLocation, Bearings bearing)
        {
            this.location = startLocation;
            this.bearing = bearing;
        }

        public Location CurrentLocation
        {
            get => location;
        }

        public Location PredictedLocation
        {
            get
            {
                switch (bearing)
                {
                    case Bearings.North:
                        return new Location(location.X, location.Y + 1);
                    case Bearings.East:
                        return new Location(location.X + 1, location.Y);
                    case Bearings.South:
                        return new Location(location.X, location.Y - 1);
                    case Bearings.West:
                        return new Location(location.X - 1, location.Y);
                    default:
                        throw new UnknownBearingException($"Unknown bearing {bearing}");
                }
            }
        }

        public string State {
            get => $"{location.X} {location.Y} {bearing.ToString()[0]}";
        }

        public void TurnLeft()
        {
            bearing = bearing == Bearings.North
                ? Bearings.West
                : bearing - 1;
        }

        public void TurnRight()
        {
            bearing = bearing == Bearings.West
                ? bearing = Bearings.North
                : bearing + 1;
        }

        public void Move()
        {
            switch (bearing)
            {
                case Bearings.North:
                    location.Y++;
                    break;
                case Bearings.East:
                    location.X++;
                    break;
                case Bearings.South:
                    location.Y--;
                    break;
                case Bearings.West:
                    location.X--;
                    break;
            }
        }
    }
}
