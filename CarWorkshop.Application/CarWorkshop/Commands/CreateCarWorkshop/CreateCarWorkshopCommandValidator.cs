using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name should have minimum 2 characters")
                .MaximumLength(20).WithMessage("Name should have maximum 20 characters")
                .Custom((value, context) =>
                {
                    var workshop = repository.GetByName(value).Result;
                    if (workshop is not null)
                    {
                        context.AddFailure($"{value} is already taken car workshop name");
                    }
                });
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8).WithMessage("Phone number should have minimum 8 characters")
                .MaximumLength(12).WithMessage("Phone number should have maximum 12 characters");
        }
    }
}
