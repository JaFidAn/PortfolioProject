using Domain.Entities.Common;

namespace Domain.Entities;

public class Project : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Link { get; set; }
}
