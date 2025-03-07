using Application.DTOs.Projects;
using Application.Repositories;
using Application.Services.Abstracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Concretes;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateProjectDto dto)
    {
        var project = _mapper.Map<Project>(dto);
        project.Id = Guid.NewGuid();

        await _projectRepository.AddAsync(project);
        return project.Id;
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null || project.IsDeleted) return null;

        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects.Where(p => !p.IsDeleted));
    }

    public async Task<Project> UpdateAsync(Project model)
    {
        await _projectRepository.UpdateAsync(model);
        return await _projectRepository.GetByIdAsync(model.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _projectRepository.DeleteAsync(id);
    }

}
