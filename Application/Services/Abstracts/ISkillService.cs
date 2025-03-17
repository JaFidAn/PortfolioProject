using Application.DTOs.Skills;

namespace Application.Services.Abstracts;

public interface ISkillService
{
    Task<Guid> CreateAsync(CreateSkillDto dto);
    Task<SkillResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<SkillResponseDto>> GetAllAsync();
    Task UpdateAsync(UpdateSkillDto dto);
    Task DeleteAsync(Guid id);
}
