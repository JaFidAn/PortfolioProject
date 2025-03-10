using Application.Cqrs.Commands;
using Application.Cqrs.Queries;
using Application.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    private readonly ITechnologyQuery _technologyQuery;
    private readonly ITechnologyCommand _technologyCommand;

    public TechnologyRepository(ITechnologyQuery technologyQuery, ITechnologyCommand technologyCommand)
    {
        _technologyQuery = technologyQuery;
        _technologyCommand = technologyCommand;
    }

    public async Task<IEnumerable<Technology>> GetAllAsync()
    {
        return await _technologyQuery.GetAllAsync();
    }

    public async Task<Technology?> GetByIdAsync(Guid id)
    {
        return await _technologyQuery.GetByIdAsync(id);
    }

    public async Task<Guid> AddAsync(Technology technology)
    {
        return await _technologyCommand.CreateAsync(technology);
    }

    public async Task UpdateAsync(Technology technology)
    {
        await _technologyCommand.UpdateAsync(technology);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _technologyCommand.DeleteAsync(id);
    }

    public async Task<Technology?> GetByNameAsync(string name)
    {
        return await _technologyQuery.GetByNameAsync(name);
    }
}
