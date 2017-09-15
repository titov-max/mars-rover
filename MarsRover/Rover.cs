using System;

namespace MarsRover
{
    public class Rover
    {
        public Position TurnLeft(Position position)
        {
            var bearing = position.Bearing == Bearings.North
                ? Bearings.West
                : position.Bearing - 1;
            return new Position(position.X, position.Y, bearing);
        }

        public Position TurnRight(Position position)
        {
            var bearing = position.Bearing == Bearings.West
                ? Bearings.North
                : position.Bearing + 1;
            return new Position(position.X, position.Y, bearing);
        }

        public Position Move(Position position)
        {
            int x = position.X;
            int y = position.Y;

            switch (position.Bearing)
            {
                case Bearings.North:
                    y++;
                    break;
                case Bearings.East:
                    x++;
                    break;
                case Bearings.South:
                    y--;
                    break;
                case Bearings.West:
                    x--;
                    break;
            }
            return new Position(x, y, position.Bearing);
        }
    }
}
