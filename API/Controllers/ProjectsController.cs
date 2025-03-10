using Microsoft.AspNetCore.Mvc;
using Application.Services.Abstracts;
using Application.DTOs.Projects;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var projectId = await _projectService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, new { id = projectId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null)
            return NotFound($"Project with ID {id} not found");

        return Ok(project);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto dto)
    {
        if (dto == null) return BadRequest("Invalid data");

        var updatedProject = await _projectService.UpdateAsync(dto);
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await _projectService.DeleteAsync(id);
        return NoContent();
    }
}
