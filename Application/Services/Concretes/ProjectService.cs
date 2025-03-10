using Application.DTOs.Projects;
using Application.Repositories;
using Application.Services.Abstracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Concretes;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IProjectTechnologyRepository _projectTechnologyRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, ITechnologyRepository technologyRepository, IProjectTechnologyRepository projectTechnologyRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _technologyRepository = technologyRepository;
        _projectTechnologyRepository = projectTechnologyRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateProjectDto dto)
    {
        var project = _mapper.Map<Project>(dto);
        project.Id = Guid.NewGuid();

        await _projectRepository.AddAsync(project);

        foreach (var techName in dto.Technologies)
        {
            var technology = await _technologyRepository.GetByNameAsync(techName);

            if (technology == null)
            {
                technology = new Technology { Id = Guid.NewGuid(), Name = techName };
                await _technologyRepository.AddAsync(technology);
            }

            await _projectTechnologyRepository.AddAsync(project.Id, technology.Id);
        }

        return project.Id;
    }



    public async Task<ProjectResponseDto?> GetByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null || project.IsDeleted) return null;

        var projectResponse = _mapper.Map<ProjectResponseDto>(project);

        var technologyIds = await _projectTechnologyRepository.GetTechnologiesByProjectIdAsync(project.Id);
        var technologies = new List<string>();

        foreach (var techId in technologyIds)
        {
            var tech = await _technologyRepository.GetByIdAsync(techId);
            if (tech != null)
            {
                technologies.Add(tech.Name);
            }
        }

        projectResponse.Technologies = technologies;
        return projectResponse;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        var projectResponses = _mapper.Map<IEnumerable<ProjectResponseDto>>(projects.Where(p => !p.IsDeleted));

        foreach (var projectResponse in projectResponses)
        {
            var technologyIds = await _projectTechnologyRepository.GetTechnologiesByProjectIdAsync(projectResponse.Id);
            var technologies = new List<string>();

            foreach (var techId in technologyIds)
            {
                var tech = await _technologyRepository.GetByIdAsync(techId);
                if (tech != null)
                {
                    technologies.Add(tech.Name);
                }
            }

            projectResponse.Technologies = technologies;
        }

        return projectResponses;
    }

    public async Task<Project> UpdateAsync(UpdateProjectDto dto)
    {
        var project = await _projectRepository.GetByIdAsync(dto.Id);
        if (project == null)
        {
            throw new KeyNotFoundException($"Project with ID {dto.Id} not found.");
        }

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Link = dto.Link;

        await _projectRepository.UpdateAsync(project);

        await _projectTechnologyRepository.RemoveAllByProjectIdAsync(project.Id);

        foreach (var techName in dto.Technologies)
        {
            var technology = await _technologyRepository.GetByNameAsync(techName);

            if (technology == null)
            {
                technology = new Technology { Id = Guid.NewGuid(), Name = techName };
                await _technologyRepository.AddAsync(technology);
            }

            await _projectTechnologyRepository.AddAsync(project.Id, technology.Id);
        }

        return await _projectRepository.GetByIdAsync(project.Id);
    }


    public async Task DeleteAsync(Guid id)
    {
        await _projectTechnologyRepository.RemoveAllByProjectIdAsync(id);

        await _projectRepository.DeleteAsync(id);
    }

}
