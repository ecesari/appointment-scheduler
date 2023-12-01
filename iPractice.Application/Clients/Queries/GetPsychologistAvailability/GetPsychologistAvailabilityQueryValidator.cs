using FluentValidation;
using iPractice.Application.Clients.Commands.CreateAvailability;

namespace iPractice.Application.Psychologists.Commands.CreateAvailability
{
    class GetPsychologistAvailabilityQueryValidator : AbstractValidator<GetPsychologistAvailabilityQuery>
    {
        public GetPsychologistAvailabilityQueryValidator()
        {
            RuleFor(v => v.ClientId).NotNull().WithMessage("Client is required.");
        }
    }
}
