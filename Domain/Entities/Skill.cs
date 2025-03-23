using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Skill : BaseEntity
{
    public required string Name { get; set; }
    public SkillLevel Level { get; set; }
}
