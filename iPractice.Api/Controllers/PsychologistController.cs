﻿using System;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Psychologists.Commands.CreateAvailability;
using iPractice.Application.Psychologists.Commands.UpdateAvailability;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistController : ControllerBase
    {
        private readonly IMediator mediator;

        public PsychologistController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public string Get()
        {
            return "Success!";
        }

        /// <summary>
        /// Add a block of time during which the psychologist is available during normal business hours
        /// </summary>
        /// <param name="psychologistId"></param>
        /// <param name="availability">Availability</param>
        /// <returns>Ok if the availability was created</returns>
        [HttpPost("{psychologistId}/availability")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateAvailability([FromRoute] long psychologistId, [FromBody] Availability availability)
        {
           await mediator.Send(new CreateAvailabilityCommand { PsychologistId = psychologistId, TimeFrom = availability.TimeFrom, TimeTo = availability.TimeTo });
            return Ok();
        }

        /// <summary>
        /// Update availability of a psychologist
        /// </summary>
        /// <param name="psychologistId">The psychologist's ID</param>
        /// <param name="availabilityId">The ID of the availability block</param>
        /// <returns>List of availability slots</returns>
        [HttpPut("{psychologistId}/availability/{availabilityId}")]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Availability>> UpdateAvailability([FromRoute] long psychologistId, [FromRoute] long availabilityId, [FromBody] Availability availability)
        {
            await mediator.Send(new UpdateAvailabilityCommand { PsychologistId = psychologistId, TimeFrom = availability.TimeFrom, TimeTo = availability.TimeTo });
            return Ok();
        }
    }
}
