using Application.Core;
using Application.Features.Skills.Commands;
using Application.Features.Skills.DTOs;
using Application.Features.Skills.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SkillsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<SkillDto>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        return await Mediator.Send(new GetSkillList.Query { Params = paginationParams });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SkillDto>> GetDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetSkillDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateSkillDto skillDto)
    {
        return HandleResult(await Mediator.Send(new CreateSkill.Command { SkillDto = skillDto }));
    }

    [HttpPut]
    public async Task<ActionResult> Edit(EditSkillDto skillDto)
    {
        return HandleResult(await Mediator.Send(new EditSkill.Command { SkillDto = skillDto }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteSkill.Command { Id = id }));
    }
}
