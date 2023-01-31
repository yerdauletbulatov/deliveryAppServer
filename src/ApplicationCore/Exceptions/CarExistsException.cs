using System;

namespace ApplicationCore.Exceptions
{
    public class CarExistsException: Exception
    {
        public CarExistsException(string message)
            : base(message)
        { }
    }
}