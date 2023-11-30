using AutoMapper;
using iPractice.Application.Common.Models;
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
            var client = await repository.GetByIdAsync(request.ClientId);
            var psychologists = client.Psychologists;
            var appointmentIdList = psychologists.Select(appointment => appointment.Id).ToList();
            //TODO:refactor query
            var timeSlots = psychologists.Select(psychologist=> psychologist.Availability.Where(appointment => !appointmentIdList.Contains(appointment.Id))).ToList();
            var returnModel = mapper.Map<List<PsychologistAvailabilityResponse>>(timeSlots);
            return returnModel;
        }
    }
}
