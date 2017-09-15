using System;

namespace MarsRover.Exceptions
{
    public class UnknownBearingException : Exception 
    {
        public UnknownBearingException()
        {
        }

        public UnknownBearingException(string message) 
            : base(message) { }

        public UnknownBearingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}