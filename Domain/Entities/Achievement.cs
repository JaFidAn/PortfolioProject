using Domain.Entities.Common;

namespace Domain.Entities;

public class Achievement : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
}
