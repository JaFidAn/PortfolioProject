using Application.Core;
using Application.Features.Achievements.Commands;
using Application.Features.Achievements.DTOs;
using Application.Features.Achievements.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AchievementsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<AchievementDto>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        return await Mediator.Send(new GetAchievementList.Query { Params = paginationParams });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AchievementDto>> GetDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetAchievementDetails.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateAchievementDto achievementDto)
    {
        return HandleResult(await Mediator.Send(new CreateAchievement.Command { AchievementDto = achievementDto }));
    }

    [HttpPut]
    public async Task<ActionResult> Edit(EditAchievementDto achievementDto)
    {
        return HandleResult(await Mediator.Send(new EditAchievement.Command { AchievementDto = achievementDto }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteAchievement.Command { Id = id }));
    }
}
