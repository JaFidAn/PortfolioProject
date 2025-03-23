using Application.Features.Projects.DTOs;
using FluentValidation;

namespace Application.Features.Projects.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Project title is required.")
            .MaximumLength(200).WithMessage("Project title must be at most 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Project description is required.");

        RuleFor(x => x.Link)
            .NotEmpty().WithMessage("Project link is required.")
            .MaximumLength(300).WithMessage("Project link must be at most 300 characters.")
            .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _)).WithMessage("Project link must be a valid URL.");

        RuleFor(x => x.TechnologyIds)
            .NotNull().WithMessage("At least one technology must be selected.")
            .Must(ids => ids.Any()).WithMessage("At least one technology must be selected.");
    }
}
