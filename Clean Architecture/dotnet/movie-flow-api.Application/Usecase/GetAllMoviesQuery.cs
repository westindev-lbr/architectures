using MediatR;
using movie_flow_api.Application.Dto;
using movie_flow_api.Domain.Common;

namespace movie_flow_api.Application.Usecase;

public record GetAllMoviesQuery() : IRequest<IActionResponse<IEnumerable<MovieDto>>>;
