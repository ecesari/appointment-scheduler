using FluentValidation;
using iPractice.Application.Clients.Commands.CreateAppointment;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(v => v.ClientId).NotNull().WithMessage("Client is required.");
            RuleFor(v => v.TimeSlotId).NotNull().WithMessage("Time slot is required.");
            RuleFor(v => v.PsychologistId).NotNull().WithMessage("Psychologist is required.");
        }
    }
}
