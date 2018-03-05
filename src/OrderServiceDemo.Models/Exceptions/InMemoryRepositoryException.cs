using System;

namespace OrderServiceDemo.Models.Exceptions
{
    public class InMemoryRepositoryException : Exception
    {
        public InMemoryRepositoryException(string message)
            : base(message)
        {

        }
    }
}
