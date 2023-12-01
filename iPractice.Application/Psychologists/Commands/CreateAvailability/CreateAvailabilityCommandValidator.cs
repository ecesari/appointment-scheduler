using FluentValidation;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    class CreateAvailabilityCommandValidator : AbstractValidator<CreateAvailabilityCommand>
    {
        public CreateAvailabilityCommandValidator()
        {
            RuleFor(v => v.TimeFrom).NotNull().WithMessage("Starting time required.");
            RuleFor(v => v.TimeTo).NotNull().WithMessage("Ending time required.");
            RuleFor(v => v.PsychologistId).NotNull().WithMessage("Psychologist required.");
        }
    }
}
