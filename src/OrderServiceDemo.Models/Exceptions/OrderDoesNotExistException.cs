using System;

namespace OrderServiceDemo.Models.Exceptions
{
    public class OrderDoesNotExistException : Exception
    {
        public OrderDoesNotExistException(string message)
            : base(message)
        {

        }

        public OrderDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}