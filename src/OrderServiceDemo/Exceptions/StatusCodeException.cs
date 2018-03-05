using System;

namespace OrderServiceDemo.Exceptions
{
    public class StatusCodeException : Exception
    {
        private int _statusCode;
        public StatusCodeException(int statusCode, string message = null)
            : base(message)
        {
            _statusCode = statusCode;
        }

        public StatusCodeException(int statusCode, string message, Exception inner)
            : base(message, inner)
        {
            _statusCode = statusCode;
        }

        public int StatusCode { get { return _statusCode; } }
    }
}
