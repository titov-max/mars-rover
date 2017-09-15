using System;

namespace MarsRover
{
    public class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public bool BelongsGrid(int x, int y)
        {
            return 0 <= X && X <= x 
                && 0 <= Y && Y <= y;
        }
    }
}
