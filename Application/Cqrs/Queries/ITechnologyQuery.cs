using Domain.Entities;

namespace Application.Cqrs.Queries;

public interface ITechnologyQuery
{
    Task<IEnumerable<Technology>> GetAllAsync();
    Task<Technology?> GetByIdAsync(Guid id);
    Task<Technology?> GetByNameAsync(string name);
}
