using Application.DTOs.Skills;
using Application.Repositories;
using Application.Services.Abstracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Concretes;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;

    public SkillService(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateSkillDto dto)
    {
        var skill = _mapper.Map<Skill>(dto);
        skill.Id = Guid.NewGuid();

        await _skillRepository.AddAsync(skill);
        return skill.Id;
    }

    public async Task<SkillResponseDto?> GetByIdAsync(Guid id)
    {
        var skill = await _skillRepository.GetByIdAsync(id);
        if (skill == null || skill.IsDeleted) return null;

        return _mapper.Map<SkillResponseDto>(skill);
    }

    public async Task<IEnumerable<SkillResponseDto>> GetAllAsync()
    {
        var skills = await _skillRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SkillResponseDto>>(skills.Where(s => !s.IsDeleted));
    }

    public async Task UpdateAsync(UpdateSkillDto dto)
    {
        var skill = await _skillRepository.GetByIdAsync(dto.Id);
        if (skill == null)
        {
            throw new KeyNotFoundException($"Skill with ID {dto.Id} not found.");
        }

        skill.Name = dto.Name;
        skill.Level = dto.Level;
        await _skillRepository.UpdateAsync(skill);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _skillRepository.DeleteAsync(id);
    }
}
