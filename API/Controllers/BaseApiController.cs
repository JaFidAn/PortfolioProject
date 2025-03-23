using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/portfolio/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator
             ??= HttpContext.RequestServices.GetService<IMediator>()
             ?? throw new InvalidOperationException("IMediator service is unavaliable");

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (!result.IsSuccess && result.Code == 404)
            {
                return NotFound(new
                {
                    result.Error,
                    result.Code
                });
            }

            if (result.IsSuccess)
            {
                var type = typeof(T);
                if (type == typeof(string) || type == typeof(Guid) || type == typeof(int))
                {
                    return Ok(new
                    {
                        id = result.Value,
                        message = result.Message
                    });
                }

                if (type == typeof(Unit))
                {
                    return Ok(new
                    {
                        message = result.Message
                    });
                }

                return Ok(result.Value);
            }

            return BadRequest(new
            {
                result.Error,
                result.Code
            });
        }
    }
}
