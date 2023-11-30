using iPractice.Application.Common.Interfaces;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    public class CreateAvailabilityCommand : IRequest
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public long PsychologistId { get; set; }
    }

    public class CreateAvailabilityCommandHandler : IRequestHandler<CreateAvailabilityCommand>
    {
        private readonly IPsychologistRepository repository;        
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly ITimeSplitter timeSplitter;

        public CreateAvailabilityCommandHandler(IPsychologistRepository repository, ITimeSlotRepository timeSlotRepository, ITimeSplitter timeSplitter)
        {
            this.repository = repository;
            this.timeSlotRepository = timeSlotRepository;
            this.timeSplitter = timeSplitter;
        }

        public async Task Handle(CreateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            //TODO: add error handling
            var psychologist = await repository.GetByIdAsync(request.PsychologistId);
            //create timeslots
            var timeslots = timeSplitter.Split(request.TimeFrom, request.TimeTo, 30);
            await timeSlotRepository.AddAsync(timeslots);
            //add timeslots to psychologists
            psychologist.Availability.AddRange(timeslots);
            await repository.UpdateAsync(psychologist);
            //TODO: return psychologist id or not
            //maybe call to notify client
        }
    }
}
