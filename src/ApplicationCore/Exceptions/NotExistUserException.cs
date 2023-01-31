using System;

namespace ApplicationCore.Exceptions
{
    public class NotExistUserException : Exception
    {
        public NotExistUserException(string message)
            : base(message)
        { }
    }
}