using Application.DTOs.Technologies;
using Domain.Entities;

namespace Application.Services.Abstracts;

public interface ITechnologyService
{
    Task<Guid> CreateAsync(CreateTechnologyDto dto);
    Task<TechnologyResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TechnologyResponseDto>> GetAllAsync();
    Task<Technology> UpdateAsync(Technology model);
    Task DeleteAsync(Guid id);
}
