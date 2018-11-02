using System;

namespace OrderServiceDemo.Models.Exceptions
{
    public class OrderIsAlreadyCanceled : Exception
    {
        public OrderIsAlreadyCanceled(string message)
            : base(message)
        {

        }

        public OrderIsAlreadyCanceled(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}