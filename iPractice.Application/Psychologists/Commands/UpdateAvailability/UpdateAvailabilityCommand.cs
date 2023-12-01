using iPractice.Application.Common.Exceptions;
using iPractice.Application.Common.Services;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Psychologists.Commands.UpdateAvailability
{
    public class UpdateAvailabilityCommand : IRequest<long>
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public long PsychologistId { get; set; }
        public List<long> TimeSlots { get; set; }
    }

    public class UpdateAvailabilityCommandHandler : IRequestHandler<UpdateAvailabilityCommand, long>
    {
        private readonly IPsychologistRepository repository;        
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly ITimeSplitter timeSplitter;

        public UpdateAvailabilityCommandHandler(IPsychologistRepository repository, ITimeSlotRepository timeSlotRepository, ITimeSplitter timeSplitter)
        {
            this.repository = repository;
            this.timeSlotRepository = timeSlotRepository;
            this.timeSplitter = timeSplitter;
        }

        public async Task<long> Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            //TODO:check for existing appointments
            //check existing appointments
            var psychologist = await repository.GetByIdAsync(request.PsychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), request.PsychologistId); 
            var timeslots = timeSplitter.Split(request.TimeFrom, request.TimeTo, 30);
            await timeSlotRepository.AddAsync(timeslots);
            psychologist.Availability.AddRange(timeslots);
            await repository.UpdateAsync(psychologist);
            return psychologist.Id;
        }
    }
}
