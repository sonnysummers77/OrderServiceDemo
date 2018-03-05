using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderServiceDemo.Exceptions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceDemo.Attributes
{
    public class HandleErrorAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var hostingEnvironment = context.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            var loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<HandleErrorAttribute>();

            bool includeStackTraces = false;
            try
            {
                includeStackTraces = configuration.GetValue("IncludeStacktraceOnErrors", false) || hostingEnvironment.IsDevelopment();
            }
            catch { }

            var currentException = context.Exception;
            StringBuilder sbMessage = new StringBuilder();

            sbMessage.AppendLine("An error occurred in the web api request.");

            while (currentException != null)
            {
                sbMessage.AppendLine("Error: " + currentException.Message);

                if (includeStackTraces)
                    sbMessage.AppendLine("StackTrace: " + currentException.StackTrace);

                currentException = currentException.InnerException;
            }

            var result = new ObjectResult(sbMessage.ToString());

            if (context.Exception is StatusCodeException)
                result.StatusCode = (context.Exception as StatusCodeException).StatusCode;
            else
                result.StatusCode = (int?)HttpStatusCode.InternalServerError;

            result.ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection()
            {
                new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("text/plain")
            };

            context.ExceptionHandled = true;
            context.Result = result;

            logger.LogError(0, context.Exception, sbMessage.ToString());

            return Task.FromResult(0);
        }
    }
}
