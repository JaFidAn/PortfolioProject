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
             ?? throw new InvalidOperationException("IMediator service is unavailable");

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            // Not Found
            if (!result.IsSuccess && result.Code == 404)
            {
                return NotFound(new
                {
                    result.Error,
                    result.Code
                });
            }

            var type = typeof(T);

            // Success with only message (e.g., Result<Unit>)
            if (result.IsSuccess && type == typeof(Unit))
            {
                return Ok(new { message = result.Message });
            }

            // Success with non-null value
            if (result.IsSuccess && result.Value is not null)
            {
                // For string / int / Guid returns: id + message
                if (type == typeof(string) || type == typeof(Guid) || type == typeof(int))
                {
                    return Ok(new
                    {
                        id = result.Value,
                        message = result.Message
                    });
                }

                // Otherwise, return just the object
                return Ok(result.Value);
            }

            // General Bad Request
            return BadRequest(new
            {
                result.Error,
                result.Code
            });
        }
    }
}
