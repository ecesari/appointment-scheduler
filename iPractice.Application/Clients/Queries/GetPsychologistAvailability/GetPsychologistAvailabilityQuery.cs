using AutoMapper;
using iPractice.Application.Clients.Queries.GetPsychologistAvailability;
using iPractice.Application.Common.Exceptions;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Clients.Commands.CreateAvailability
{
    public class GetPsychologistAvailabilityQuery : IRequest<List<PsychologistAvailabilityResponse>>
    {
        public long ClientId { get; set; }
    }

    public class GetPsychologistAvailabilityQueryHandler : IRequestHandler<GetPsychologistAvailabilityQuery, List<PsychologistAvailabilityResponse>>
    {
        private readonly IClientRepository repository;
        private readonly IMapper mapper;
        public GetPsychologistAvailabilityQueryHandler(IClientRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PsychologistAvailabilityResponse>> Handle(GetPsychologistAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.ClientId) ?? throw new EntityNotFoundException(nameof(Client), request.ClientId);
            var psychologists = client.Psychologists;
            var appointmentIdList = psychologists.Select(appointment => appointment.Id).ToList();
            var timeSlots = psychologists.Select(psychologist => psychologist.Availability.Where(appointment => !appointmentIdList.Contains(appointment.Id))).ToList();
            //TODO:refactor query
            var responseList = new List<PsychologistAvailabilityResponse>();
            foreach (var psychologist in psychologists) {
                foreach (var timeslot in psychologist.Availability)
                {
                    if (appointmentIdList.Contains(timeslot.Id))                 
                        continue;
                    
                    var response = new PsychologistAvailabilityResponse
                    {
                        PsychologistId = psychologist.Id,
                        PsychologistName = psychologist.Name,
                        TimeFrom = timeslot.TimeFrom,
                        TimeTo = timeslot.TimeTo,
                        TimeSlotId = timeslot.Id
                    };
                    responseList.Add(response);
                }
            }
            return responseList;
        }
    }
}
