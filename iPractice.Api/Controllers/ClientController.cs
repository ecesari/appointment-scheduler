using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using iPractice.Api.Models;
using iPractice.Application.Clients.Commands.CreateAppointment;
using iPractice.Application.Clients.Commands.CreateAvailability;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public ClientController(ILogger<ClientController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all clients.
        /// </summary>
        /// <returns>All clients</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> Get()
        {
            var response = await _mediator.Send(new GetClientsQuery());
            var result = _mapper.Map<List<ClientViewModel>>(response);
            return result;
        }

        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<PsychologistAvailability>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PsychologistAvailability>>> GetAvailableTimeSlots(long clientId)
        {
            var response = await _mediator.Send(new GetPsychologistAvailabilityQuery { ClientId = clientId });
            var result = _mapper.Map<List<PsychologistAvailability>>(response);
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
        public async Task<ActionResult> CreateAppointment(long clientId, [FromBody] PsychologistAvailability timeSlot)
        {
             await _mediator.Send(new CreateAppointmentCommand { ClientId = clientId, PsychologistId = timeSlot.PsychologistId, TimeSlotId = timeSlot.TimeSlotId });
            return Ok();
        }
    }
}
