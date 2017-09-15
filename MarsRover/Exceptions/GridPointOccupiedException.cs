using System;

namespace MarsRover.Exceptions
{
    public class GridPointOccupiedException : Exception 
    {
        public GridPointOccupiedException()
        {
        }

        public GridPointOccupiedException(string message) 
            : base(message) { }

        public GridPointOccupiedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}