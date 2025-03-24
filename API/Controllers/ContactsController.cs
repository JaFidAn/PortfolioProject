using Application.Core;
using Application.Features.Contacts.Commands;
using Application.Features.Contacts.DTOs;
using Application.Features.Contacts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ContactsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ContactDto>>> GetAll([FromQuery] PaginationParams paginationParams)
    {
        return await Mediator.Send(new GetContactList.Query { Params = paginationParams });
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateContactDto contactDto)
    {
        return HandleResult(await Mediator.Send(new CreateContact.Command { ContactDto = contactDto }));
    }
}
