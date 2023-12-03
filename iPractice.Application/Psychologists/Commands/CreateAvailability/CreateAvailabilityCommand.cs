using iPractice.Application.Common.Exceptions;
using iPractice.Application.Common.Services;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    public class CreateAvailabilityCommand : IRequest<long>
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public long PsychologistId { get; set; }
    }

    public class CreateAvailabilityCommandHandler : IRequestHandler<CreateAvailabilityCommand, long>
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

        public async Task<long> Handle(CreateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var psychologist = await repository.GetByIdAsync(request.PsychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), request.PsychologistId); ;
            var timeslots = timeSplitter.Split(request.TimeFrom, request.TimeTo, 30);
            await timeSlotRepository.AddAsync(timeslots);
            psychologist.Availability.AddRange(timeslots);
            await repository.UpdateAsync(psychologist);
            //call to notify client
            return psychologist.Id;
        }
    }
}
