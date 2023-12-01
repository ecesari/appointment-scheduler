using FluentValidation;
using iPractice.Application.Psychologists.Commands.UpdateAvailability;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    class UpdateAvailabilityCommandValidator : AbstractValidator<UpdateAvailabilityCommand>
    {
        public UpdateAvailabilityCommandValidator()
        {
            RuleFor(v => v.TimeFrom).NotNull().WithMessage("Starting time required.");
            RuleFor(v => v.TimeTo).NotNull().WithMessage("Ending time required.");
            RuleFor(v => v.PsychologistId).NotNull().WithMessage("Psychologist required.");
        }
    }
}
