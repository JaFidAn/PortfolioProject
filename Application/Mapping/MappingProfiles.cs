using Application.DTOs.Projects;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Project, ProjectResponseDto>();

        CreateMap<CreateProjectDto, Project>();
    }
}
