using Domain.Entities;

namespace Application.Repositories;

public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id);
    Task<IEnumerable<Skill>> GetAllAsync();
    Task<Guid> AddAsync(Skill skill);
    Task UpdateAsync(Skill skill);
    Task DeleteAsync(Guid id);
}
