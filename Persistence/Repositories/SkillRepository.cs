using Application.Cqrs.Commands;
using Application.Cqrs.Queries;
using Application.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly ISkillQuery _skillQuery;
    private readonly ISkillCommand _skillCommand;

    public SkillRepository(ISkillQuery skillQuery, ISkillCommand skillCommand)
    {
        _skillQuery = skillQuery;
        _skillCommand = skillCommand;
    }

    public async Task<IEnumerable<Skill>> GetAllAsync()
    {
        return await _skillQuery.GetAllAsync();
    }

    public async Task<Skill?> GetByIdAsync(Guid id)
    {
        return await _skillQuery.GetByIdAsync(id);
    }

    public async Task<Guid> AddAsync(Skill skill)
    {
        return await _skillCommand.CreateAsync(skill);
    }

    public async Task UpdateAsync(Skill skill)
    {
        await _skillCommand.UpdateAsync(skill);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _skillCommand.DeleteAsync(id);
    }
}
