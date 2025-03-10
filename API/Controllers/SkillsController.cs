using Microsoft.AspNetCore.Mvc;
using Application.Services.Abstracts;
using Application.DTOs.Skills;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSkill([FromBody] CreateSkillDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var skillId = await _skillService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetSkillById), new { id = skillId }, new { id = skillId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSkills()
    {
        var skills = await _skillService.GetAllAsync();
        return Ok(skills);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(Guid id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (skill == null)
            return NotFound($"Skill with ID {id} not found");

        return Ok(skill);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSkill([FromBody] UpdateSkillDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        await _skillService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id)
    {
        await _skillService.DeleteAsync(id);
        return NoContent();
    }
}
