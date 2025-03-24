using Application.Features.Achievements.DTOs;
using Application.Features.Auth.DTOs;
using Application.Features.Contacts.DTOs;
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

        CreateMap<CreateAchievementDto, Achievement>();
        CreateMap<EditAchievementDto, Achievement>();
        CreateMap<Achievement, AchievementDto>();

        CreateMap<CreateContactDto, Contact>();
        CreateMap<Contact, ContactDto>();

        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
    }
}
