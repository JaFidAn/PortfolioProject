using Application.Features.Projects.DTOs;
using Application.Features.Skills.DTOs;
using Application.Features.Technologies.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProjectDto, Project>()
            .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore());
        CreateMap<EditProjectDto, Project>()
            .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore());
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Technologies,
                       opt => opt.MapFrom(src =>
                           src.ProjectTechnologies.Select(pt => pt.Technology.Name)));

        CreateMap<CreateTechnologyDto, Technology>();
        CreateMap<EditTechnologyDto, Technology>();
        CreateMap<Technology, TechnologyDto>();

        CreateMap<CreateSkillDto, Skill>();
        CreateMap<EditSkillDto, Skill>();
        CreateMap<Skill, SkillDto>();
    }
}
