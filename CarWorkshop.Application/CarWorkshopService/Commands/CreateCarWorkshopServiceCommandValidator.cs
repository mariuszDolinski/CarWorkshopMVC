using FluentValidation;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    internal class CreateCarWorkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
    {
        public CreateCarWorkshopServiceCommandValidator() 
        {
            RuleFor(s => s.Cost).NotEmpty().WithMessage("Cost cannot be empty");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(s => s.CarWorkshopEncodedName).NotEmpty().NotNull();
        }
    }
}
