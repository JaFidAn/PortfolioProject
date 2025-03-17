using Domain.Entities;

namespace Application.Cqrs.Queries;

public interface ISkillQuery
{
    Task<IEnumerable<Skill>> GetAllAsync();
    Task<Skill?> GetByIdAsync(Guid id);
}
