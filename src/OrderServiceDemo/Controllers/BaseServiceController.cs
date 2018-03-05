using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderServiceDemo.Exceptions;
using System;
using System.Text;

namespace OrderServiceDemo.Controllers
{
    public abstract class BaseServiceController : Controller
    {
        private bool includeStackTraces = false;

        protected StatusCodeException BuildExceptionResponse(System.Net.HttpStatusCode statusCode, Exception ex)
        {
            try
            {
                var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                includeStackTraces = config["IncludeStacktraceOnErrors"]?.ToLower() == "true";
            }
            catch { }

            var currentException = ex;
            StringBuilder sbMessage = new StringBuilder();

            while (currentException != null)
            {
                sbMessage.AppendLine("Error: " + currentException.Message);

                if (includeStackTraces)
                    sbMessage.AppendLine("StackTrace: " + currentException.StackTrace);

                currentException = currentException.InnerException;
            }

            return new StatusCodeException((int)statusCode, ex.Message, ex);
        }

        protected StatusCodeException BuildExceptionResponse(System.Net.HttpStatusCode statusCode, String ErrorMessage)
        {
            return new StatusCodeException((int)statusCode, ErrorMessage);
        }
    }
}
