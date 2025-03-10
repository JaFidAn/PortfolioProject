using Domain.Entities;

namespace Application.Cqrs.Commands;

public interface ITechnologyCommand
{
    Task<Guid> CreateAsync(Technology model);
    Task UpdateAsync(Technology model);
    Task DeleteAsync(Guid id);
}
