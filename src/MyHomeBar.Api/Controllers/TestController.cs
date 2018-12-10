using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHomeBar.Domain.Exceptions;
using MyHomeBar.Logging;
using Serilog;

namespace MyHomeBar.Api.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("/test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSpeakers()
        {

            throw new UnexpectedException("Test UnexpectedException");
        }
    }
}
