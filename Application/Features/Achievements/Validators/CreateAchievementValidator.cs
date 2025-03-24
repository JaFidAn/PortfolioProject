using Application.Features.Achievements.DTOs;
using FluentValidation;

namespace Application.Features.Achievements.Validators;

public class CreateAchievementValidator : AbstractValidator<CreateAchievementDto>
{
    public CreateAchievementValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Achievement title is required.")
            .MaximumLength(200).WithMessage("Achievement title must be at most 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Achievement description is required.")
            .MaximumLength(1000).WithMessage("Achievement description must be at most 1000 characters.");
    }
}
