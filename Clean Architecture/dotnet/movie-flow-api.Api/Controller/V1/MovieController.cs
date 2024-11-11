using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using movie_flow_api.Application.Usecase;

namespace movie_flow_api.Api.Controller;

[ApiController]
[ApiVersion("1.0")]
public class MovieController(IMediator mediator) : AbstractController(mediator)
{
     [HttpGet("movies")]
    public async Task<IActionResult> GetAllMovies()
    {
        return await Send(new GetAllMoviesQuery());
    }
}