using System;

namespace OrderServiceDemo.Models.Exceptions
{
    [Serializable()]
    public class OrderIsAlreadyCanceledException : Exception
    {
        protected OrderIsAlreadyCanceledException(string message)
            : base(message)
        {

        }
    }
}