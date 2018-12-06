using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHomeBar.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Api.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger logger;
        public TestController(ILogger logger)
        {
            this.logger = logger;
        }
        [HttpGet("/test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSpeakers()
        {
            this.logger.Log("hola");
            return Ok("TEST");
        }
    }
}
