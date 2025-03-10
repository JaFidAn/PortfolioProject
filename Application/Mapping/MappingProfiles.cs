using Application.DTOs.Projects;
using Application.DTOs.Technologies;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Project, ProjectResponseDto>();

        CreateMap<CreateProjectDto, Project>();

        CreateMap<Technology, TechnologyResponseDto>();

        CreateMap<CreateTechnologyDto, Technology>();
    }
}
