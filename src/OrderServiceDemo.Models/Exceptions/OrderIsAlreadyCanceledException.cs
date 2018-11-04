using System;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable]
    public class OrderIsAlreadyCanceledException : Exception
    {
        public OrderIsAlreadyCanceledException(string message)
            : base(message)
        {

        }
    }
}