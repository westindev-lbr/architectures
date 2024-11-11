
using MediatR;
using Microsoft.AspNetCore.Mvc;
using movie_flow_api.Application.Common.Exceptions;
using movie_flow_api.Domain.Common;

namespace movie_flow_api.Api.Controller;


[Route("api/v{version:apiVersion}/[controller]")]
public class AbstractController : ControllerBase
{
        private readonly IMediator _mediator;

        public AbstractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Send<T>(IRequest<T> request)
    {
        try
        {
            return Ok(await _mediator.Send(request));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.Select(x => x.Value));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {

            if (ex.GetType() == typeof(BadRequestException))
            {
                return StatusCode(StatusCodes.Status400BadRequest, await SetErrorMessageResponse<T>(StatusCodes.Status400BadRequest, ex.Message));
            }

            if (ex.GetType() == typeof(UnauthorizedAccessException))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, await SetErrorMessageResponse<T>(StatusCodes.Status401Unauthorized, ex.Message));
            }

            if (ex.GetType() == typeof(ServiceUnavailableException))
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, await SetErrorMessageResponse<T>(StatusCodes.Status503ServiceUnavailable, ex.Message));
            }

            if (ex.GetType() == typeof(InternalServerException))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, await SetErrorMessageResponse<T>(StatusCodes.Status500InternalServerError, ex.Message));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, await SetErrorMessageResponse<T>(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }

        private Task<ActionResponse<T>> SetErrorMessageResponse<T>(int statusCodes, string message)
    {
        ActionResponse<T> response = new()
        {
            ResultCode = statusCodes,
            Message = [message],
            Exception = [new AppException(message)]
        };
        return Task.FromResult(response);
    }

    
}