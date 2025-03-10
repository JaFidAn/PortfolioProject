using Microsoft.AspNetCore.Mvc;
using Application.Services.Abstracts;
using Application.DTOs.Technologies;
using Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class TechnologiesController : ControllerBase
{
    private readonly ITechnologyService _technologyService;

    public TechnologiesController(ITechnologyService technologyService)
    {
        _technologyService = technologyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTechnology([FromBody] CreateTechnologyDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var technologyId = await _technologyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetTechnologyById), new { id = technologyId }, new { id = technologyId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTechnologies()
    {
        var technologies = await _technologyService.GetAllAsync();
        return Ok(technologies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTechnologyById(Guid id)
    {
        var technology = await _technologyService.GetByIdAsync(id);
        if (technology == null)
            return NotFound($"Technology with ID {id} not found");

        return Ok(technology);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTechnology([FromBody] Technology technology)
    {
        var result = await _technologyService.UpdateAsync(technology);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTechnology(Guid id)
    {
        await _technologyService.DeleteAsync(id);
        return NoContent();
    }
}
