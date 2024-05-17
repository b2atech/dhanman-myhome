using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateComplaint;

public class CreateComplaintCommandValidator : AbstractValidator<CreateComplaintCommand>
{
    public CreateComplaintCommandValidator(IComplaintRepository complaintRepository)
    {
        RuleFor(c => c.Subject)
            .NotEmpty().WithMessage("The Subject is required");

        RuleFor(c => c.PrefferedTime)
            .NotEmpty().WithMessage("The Preffered Time is required");

        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("The Category Id is required");

        RuleFor(c => c.SubCategoryId)
            .NotEmpty().WithMessage("The Sub Category Id is required");

        RuleFor(c => c.PriorityId)
            .NotEmpty().WithMessage("The Priority Id is required");

        RuleFor(c => c.DepartmentId)
            .NotEmpty().WithMessage("The Department Id is required");

        RuleFor(c => c.OccuredDate)
            .NotEmpty().WithMessage("The Occured Date is required");

        RuleFor(c => c.PrefferedDate)
            .NotEmpty().WithMessage("The Preffered Date is required");
    }
}