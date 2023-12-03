using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using iPractice.Application.Clients.Commands.CreateAppointment;
using iPractice.Application.Clients.Commands.CreateAvailability;
using iPractice.Application.Clients.Queries.GetClients;
using iPractice.Application.Clients.Queries.GetPsychologistAvailability;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator mediator;


        public ClientController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        /// <summary>
        /// Get all clients.
        /// </summary>
        /// <returns>All clients</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientResponse>>> Get()
        {
            var result = await mediator.Send(new GetClientsQuery());
            return result;
        }

        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<PsychologistAvailabilityResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<PsychologistAvailabilityResponse>>> GetAvailableTimeSlots(long clientId)
        {
            var result = await mediator.Send(new GetPsychologistAvailabilityQuery { ClientId = clientId });
            return result;
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="timeSlot">Identifies the client and availability slot</param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateAppointment(long clientId, [FromBody] PsychologistAvailabilityResponse timeSlot)
        {
             await mediator.Send(new CreateAppointmentCommand { ClientId = clientId, PsychologistId = timeSlot.PsychologistId, TimeSlotId = timeSlot.TimeSlotId });
            return Ok();
        }
    }
}
