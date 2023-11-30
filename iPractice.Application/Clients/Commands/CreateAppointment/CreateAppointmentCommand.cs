using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Clients.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest
    {
        public long PsychologistId { get; set; }
        public long ClientId { get; set; }
        public long TimeSlotId { get; set; }
    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand>
    {
        private readonly IClientRepository repository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IPsychologistRepository psychologistRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public CreateAppointmentCommandHandler(IPsychologistRepository psychologistRepository, ITimeSlotRepository timeSlotRepository, IClientRepository repository, IAppointmentRepository appointmentRepository)
        {
            this.psychologistRepository = psychologistRepository;
            this.timeSlotRepository = timeSlotRepository;
            this.repository = repository;
            this.appointmentRepository = appointmentRepository;
        }

        public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.ClientId);
            var timeSlot = await timeSlotRepository.GetByIdAsync(request.TimeSlotId);
            var psychologist = await psychologistRepository.GetByIdAsync(request.PsychologistId);
            var appointment = new Appointment { Client = client, Psychologist = psychologist, TimeSlot = timeSlot };
            //TODO:move timeslot update to somewhere else
            //handle concurrency
            //notify psychologist   
            await appointmentRepository.AddAsync(appointment);
        }
    }

}
 