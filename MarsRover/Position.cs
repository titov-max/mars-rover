using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class Position
    {
        private readonly Dictionary<char, Bearings> bearingsMap = new Dictionary<char, Bearings>()
        {
            { 'N', Bearings.North },
            { 'E', Bearings.East },
            { 'S', Bearings.South },
            { 'W', Bearings.West }
        };

        public Position() {
            X = 0;
            Y = 0;
            Bearing = Bearings.North;
        }

        public Position(int x, int y, char bearing)
        {
            X = x;
            Y = y;
            if (!bearingsMap.ContainsKey(bearing))
            {
                throw new Exception($"Unknown bearing {bearing}");
            }
            Bearing = bearingsMap[bearing];
        }

        public Position(int x, int y, Bearings bearing)
        {
            X = x;
            Y = y;
            Bearing = bearing;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Bearings Bearing { get; set; }
    }
}
