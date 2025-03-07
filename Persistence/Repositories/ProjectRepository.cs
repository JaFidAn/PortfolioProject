using Application.Cqrs.Commands;
using Application.Cqrs.Queries;
using Application.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IProjectQuery _projectQuery;
    private readonly IProjectCommand _projectCommand;

    public ProjectRepository(IProjectQuery projectQuery, IProjectCommand projectCommand)
    {
        _projectQuery = projectQuery;
        _projectCommand = projectCommand;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _projectQuery.GetAllAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _projectQuery.GetByIdAsync(id);
    }

    public async Task<Guid> AddAsync(Project project)
    {
        return await _projectCommand.CreateAsync(project);
    }

    public async Task UpdateAsync(Project project)
    {
        await _projectCommand.UpdateAsync(project);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _projectCommand.DeleteAsync(id);
    }

}
