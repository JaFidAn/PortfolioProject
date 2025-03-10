using Application.DTOs.Projects;
using Domain.Entities;

namespace Application.Services.Abstracts;

public interface IProjectService
{
    Task<Guid> CreateAsync(CreateProjectDto dto);
    Task<ProjectResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<Project> UpdateAsync(UpdateProjectDto dto);
    Task DeleteAsync(Guid id);
}
