using Application.Features.Skills.DTOs;
using FluentValidation;

namespace Application.Features.Skills.Validators;

public class EditSkillValidator : AbstractValidator<EditSkillDto>
{
    public EditSkillValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Skill ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Skill name is required.")
            .MaximumLength(100).WithMessage("Skill name must be at most 100 characters.");

        RuleFor(x => x.Level)
            .IsInEnum().WithMessage("Skill level is not valid.");
    }
}
