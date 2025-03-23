using Application.Features.Technologies.DTOs;
using FluentValidation;

namespace Application.Features.Technologies.Validators;

public class CreateTechnologyValidator : AbstractValidator<CreateTechnologyDto>
{
    public CreateTechnologyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Technology name is required.")
            .MaximumLength(100).WithMessage("Technology name must be at most 100 characters.");
    }
}
