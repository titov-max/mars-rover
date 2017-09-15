using System;

namespace MarsRover.Exceptions
{
    public class OutOfGridException : Exception 
    {
        public OutOfGridException()
        {
        }

        public OutOfGridException(string message) 
            : base(message) { }

        public OutOfGridException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}