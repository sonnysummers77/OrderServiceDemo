using System;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable()]
     class OrderDoesNotExistException : Exception
    {
        protected OrderDoesNotExistException(string message)
            : base(message)
        {

        }
    }
}