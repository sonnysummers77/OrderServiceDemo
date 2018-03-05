using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrderServiceDemo.Controllers
{
    public class HealthcheckController : BaseServiceController
    {
        [HttpGet]
        [Route("")]
        [Route("healthcheck")]
        public Task<IActionResult> Healthcheck()
        {
            var response = Json(new
            {
                hostName = Environment.MachineName
            });

            return Task.FromResult<IActionResult>(response);
        }
    }
}
