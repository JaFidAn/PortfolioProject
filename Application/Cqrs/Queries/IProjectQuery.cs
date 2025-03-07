using Domain.Entities;

namespace Application.Cqrs.Queries;

public interface IProjectQuery
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
}
