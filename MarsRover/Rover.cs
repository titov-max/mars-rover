using System;

namespace MarsRover
{
    public class Rover
    {
        public Rover(Position position)
        {
            this.Current = position;
        }

        public Position Current { get; set; }

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
                        throw new Exception($"Unknown bearing ${Current.Bearing}");
                }
            }
        }

        public string State => $"{Current.X} {Current.Y} {Current.Bearing.ToString()[0]}";

        public void TurnLeft()
        {
            Current.Bearing = Current.Bearing == Bearings.North
                ? Bearings.West
                : Current.Bearing - 1;
        }

        public void TurnRight()
        {
            Current.Bearing = Current.Bearing == Bearings.West
                ? Bearings.North
                : Current.Bearing + 1;
        }

        public void Move()
        {
            Current = Next;
        }
    }
}
