using Domain.Enums;

namespace Application.Features.Skills.DTOs;

public class SkillDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public SkillLevel Level { get; set; }
}
