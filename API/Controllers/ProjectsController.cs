using Application.Core;
using Application.Features.Projects.Commands;
using Application.Features.Projects.DTOs;
using Application.Features.Projects.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProjectsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProjectDto>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        return await Mediator.Send(new GetProjectList.Query { Params = paginationParams });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetProjectDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateProjectDto projectDto)
    {
        return HandleResult(await Mediator.Send(new CreateProject.Command { ProjectDto = projectDto }));
    }

    [HttpPut]
    public async Task<ActionResult> Edit(EditProjectDto projectDto)
    {
        return HandleResult(await Mediator.Send(new EditProject.Command { ProjectDto = projectDto }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteProject.Command { Id = id }));
    }
}
