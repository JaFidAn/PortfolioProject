using Domain.Enums;

namespace Application.Features.Skills.DTOs;

public class CreateSkillDto
{
    public string Name { get; set; } = null!;
    public SkillLevel Level { get; set; }
}
