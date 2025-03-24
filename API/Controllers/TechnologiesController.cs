using Application.Core;
using Application.Features.Technologies.Commands;
using Application.Features.Technologies.DTOs;
using Application.Features.Technologies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class TechnologiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<TechnologyDto>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        return await Mediator.Send(new GetTechnologyList.Query { Params = paginationParams });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TechnologyDto>> GetDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetTechnologyDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateTechnologyDto technologyDto)
    {
        return HandleResult(await Mediator.Send(new CreateTechnology.Command { TechnologyDto = technologyDto }));
    }

    [HttpPut]
    public async Task<ActionResult> Edit(EditTechnologyDto technologyDto)
    {
        return HandleResult(await Mediator.Send(new EditTechnology.Command { TechnologyDto = technologyDto }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteTechnology.Command { Id = id }));
    }
}
