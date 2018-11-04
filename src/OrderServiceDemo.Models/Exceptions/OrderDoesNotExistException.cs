using System;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable]
     public class OrderDoesNotExistException : Exception
    {
        public OrderDoesNotExistException(string message)
            : base(message)
        {

        }
    }
}