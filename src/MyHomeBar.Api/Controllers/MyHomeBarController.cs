using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHomeBar.Authorization;
using MyHomeBar.Domain.Exceptions;
using MyHomeBar.Logging;
using Serilog;

namespace MyHomeBar.Api.Controllers
{
    [ApiController]
    [Authorize(Policies.Over18Years)]
    public class MyHomeBarController : ControllerBase
    {
        [HttpPost("AddDrink")]
        [Authorize(Policies.CanAddDrinks)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddDrink([FromBody] AddDrinkModel drink)
        {
            //_speakerService.AddSpeaker(speaker.Name, speaker.Description);
            return CreatedAtAction(nameof(ViewDrink), null);
        }

        [HttpGet("ViewDrink")]
        [Authorize(Policies.CanViewAndServe)]
        [Authorize(Policies.TemporaryPermission)]
        [Authorize(Policies.IsNotBanned)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ViewDrink([FromQuery] string drinkName)
        {
            //_speakerService.AddSpeaker(speaker.Name, speaker.Description);
            return Ok();
        }

        [HttpPost("ServeDrink")]
        [Authorize(Policies.CanViewAndServe)]
        [Authorize(Policies.TemporaryPermission)]
        [Authorize(Policies.IsNotBanned)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ServeDrink([FromQuery] string drinkName)
        {
            //_speakerService.AddSpeaker(speaker.Name, speaker.Description);
            return Ok();
        }

        [HttpPost("PartyDrinks")]
        [Authorize(Policies.CanViewAndServe)]
        [Authorize(Policies.TemporaryPermission)]
        [Authorize(Policies.IsNotBanned)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PartyDrinks([FromQuery] string drinkName)
        {
            //_speakerService.AddSpeaker(speaker.Name, speaker.Description);
            return Ok();
        }

        public class AddDrinkModel
        {
        }
    }
}
