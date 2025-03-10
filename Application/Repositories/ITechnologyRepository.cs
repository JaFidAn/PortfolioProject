using Domain.Entities;

namespace Application.Repositories;

public interface ITechnologyRepository
{
    Task<Technology?> GetByIdAsync(Guid id);
    Task<IEnumerable<Technology>> GetAllAsync();
    Task<Guid> AddAsync(Technology technology);
    Task UpdateAsync(Technology technology);
    Task DeleteAsync(Guid id);
    Task<Technology?> GetByNameAsync(string name);
}
