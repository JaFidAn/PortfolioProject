using Domain.Entities.Common;

namespace Domain.Entities;

public class Technology : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<ProjectTechnology> ProjectTechnologies { get; set; } = new List<ProjectTechnology>();
}
