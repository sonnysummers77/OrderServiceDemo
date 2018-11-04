using System;
using System.Runtime.Serialization;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable]
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


        protected OrderDoesNotExistException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}