using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHomeBar.Logging;

namespace MyHomeBar.Api.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        //private readonly ILogger<TestController> log;
        private readonly ILogger log;
        public TestController(ILogger log)
        {
            this.log = log;
        }

        [HttpGet("/test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSpeakers()
        {
            //this.logger.Log("hola");
            //Log.Information("In the controller!");
            this.log.Log("hola");
            return Ok("TEST");
        }
    }
}
