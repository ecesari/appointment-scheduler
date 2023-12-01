using iPractice.Application.Common.Exceptions;
using iPractice.Domain.Entities;
using iPractice.Domain.Repository;
using MediatR;

namespace iPractice.Application.Clients.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<long>
    {
        public long PsychologistId { get; set; }
        public long ClientId { get; set; }
        public long TimeSlotId { get; set; }
    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, long>
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

        public async Task<long> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.ClientId) ?? throw new EntityNotFoundException(nameof(Client), request.ClientId);
            var timeSlot = await timeSlotRepository.GetByIdAsync(request.TimeSlotId) ?? throw new EntityNotFoundException(nameof(TimeSlot), request.TimeSlotId);
            var psychologist = await psychologistRepository.GetByIdAsync(request.PsychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), request.PsychologistId);       
            var appointment = new Appointment { Client = client, Psychologist = psychologist, TimeSlot = timeSlot };
            //notify psychologist   
            await appointmentRepository.AddAsync(appointment);
            return appointment.Id;
        }
    }

}
 