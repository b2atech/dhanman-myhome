using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;
namespace Dhanman.MyHome.Application.Features.DeliveryPersons.Commands.CreateDeliveryPerson;

public class CreateDeliveryPersonCommandValidator : AbstractValidator<CreateDeliveryPersonCommand>
{
    public CreateDeliveryPersonCommandValidator(IDeliveryPersonRepository deliveryPersonRepository)
    {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The Name is required.");

            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("The Company Name is required.");

            RuleFor(c => c.MobileNumber)
                .NotEmpty().WithMessage("The Mobile Number is required.");

            RuleFor(c => c.MobileNumber)
                .MustAsync(async (mobileNumber, cancellation) => await deliveryPersonRepository.IsUniqueMobileNumber(mobileNumber, cancellation))
                .WithMessage("The Mobile Number must be unique.");
    }
}

