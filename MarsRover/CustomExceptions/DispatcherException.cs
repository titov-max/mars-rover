using System;

namespace MarsRover.CustomExceptions
{
    public class DispatcherException : Exception 
    {
        public DispatcherException()
        {
        }

        public DispatcherException(string message) 
            : base(message) { }

        public DispatcherException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}