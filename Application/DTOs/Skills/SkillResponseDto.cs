namespace Application.DTOs.Skills;

public class SkillResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Level { get; set; }
}
