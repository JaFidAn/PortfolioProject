using Domain.Entities.Common;

namespace Domain.Entities;

public class Contact : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Message { get; set; }
}
