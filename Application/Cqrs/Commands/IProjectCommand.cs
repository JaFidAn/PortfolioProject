using Domain.Entities;

namespace Application.Cqrs.Commands;

public interface IProjectCommand
{
    Task<Guid> CreateAsync(Project model);
    Task UpdateAsync(Project model);
    Task DeleteAsync(Guid id);
}

