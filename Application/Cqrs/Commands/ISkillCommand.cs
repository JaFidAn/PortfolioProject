using Domain.Entities;

namespace Application.Cqrs.Commands;

public interface ISkillCommand
{
    Task<Guid> CreateAsync(Skill model);
    Task UpdateAsync(Skill model);
    Task DeleteAsync(Guid id);
}
