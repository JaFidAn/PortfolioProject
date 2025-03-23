using Application.Features.Technologies.DTOs;
using FluentValidation;

namespace Application.Features.Technologies.Validators;

public class EditTechnologyValidator : AbstractValidator<EditTechnologyDto>
{
    public EditTechnologyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Technology ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Technology name is required.")
            .MaximumLength(100).WithMessage("Technology name must be at most 100 characters.");
    }
}
