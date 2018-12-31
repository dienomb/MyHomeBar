﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHomeBar.Api.TestRepository;
using MyHomeBar.Authorization;
using MyHomeBar.Authorization.Requirements;
using MyHomeBar.Domain.Entities;
using MyHomeBar.Domain.Exceptions;
using MyHomeBar.Logging;
using Serilog;
using System.Threading.Tasks;

namespace MyHomeBar.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policies.Over18Years)]
    public class MyHomeBarController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IDrinksRepository drinksRepository;

        public MyHomeBarController(IAuthorizationService authorizationService, IDrinksRepository drinksRepository)
        {
            this.authorizationService = authorizationService;
            this.drinksRepository = drinksRepository;
        }

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
        [Authorize(Policies.CheckIfBanned)]
        [Authorize(Policies.TemporaryPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ViewDrink([FromQuery] string drinkName)
        {
            var product = drinksRepository.Get(drinkName);

            if (product == null)
            {
                return NotFound();
            }

            var result = await authorizationService.AuthorizeAsync(User, product, new AccessContentRequirement(Scale.special));

            if (result.Succeeded)
            {
                return Ok(product);
            }

            return Forbid();
        }

        [HttpPost("ServeDrink")]
        [Authorize(Policies.CanViewAndServe)]
        [Authorize(Policies.CheckIfBanned)]
        [Authorize(Policies.TemporaryPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ServeDrink([FromQuery] string drinkName)
        {
            var product = drinksRepository.Get(drinkName);

            if (product == null)
            {
                return NotFound();
            }

            var result = await authorizationService.AuthorizeAsync(User, product, new AccessContentRequirement(Scale.special));

            if (result.Succeeded)
            {
                return Ok(product);
            }

            return Forbid();
        }

        [HttpPost("PartyDrinks")]
        [Authorize(Policies.CanViewAndServe)]
        [Authorize(Policies.TemporaryPermission)]
        [Authorize(Policies.CheckIfBanned)]
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
