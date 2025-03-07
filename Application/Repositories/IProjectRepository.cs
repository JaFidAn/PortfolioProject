using Domain.Entities;

namespace Application.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Guid> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
}

