using System;
using System.Runtime.Serialization;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable]
    public class OrderIsAlreadyCanceledException : Exception
    {
        public OrderIsAlreadyCanceledException(string message)
            : base(message)
        {

        }

        public OrderIsAlreadyCanceledException(string message, Exception inner)
            : base(message, inner)
        {

        }
        protected OrderIsAlreadyCanceledException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}