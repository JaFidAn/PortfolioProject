using Domain.Entities.Common;

namespace Domain.Entities;

public class Skill : BaseEntity
{
    public required string Name { get; set; }
    public required string Level { get; set; }
}
