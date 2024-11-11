using AutoMapper;
using MediatR;
using movie_flow_api.Application.Common.Exceptions;
using movie_flow_api.Application.Dto;
using movie_flow_api.Domain.Common;
using movie_flow_api.Domain.Interface;

namespace movie_flow_api.Application.Usecase;

public class GetAllMoviesHandler(IMovieRepository movieRepository, IMapper mapper)
        : IRequestHandler<GetAllMoviesQuery, IActionResponse<IEnumerable<MovieDto>>>
{
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IMapper _mapper = mapper;


    public async Task<IActionResponse<IEnumerable<MovieDto>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMoviesAsync();
        if (movies == null)
        {
            return new ActionResponse<IEnumerable<MovieDto>>()
            {
                Message = ["No movies found"],
                ResultCode = 404,
                Exception = [ new AppException("No movies found", new NotFoundException())]
            };
        }

        var response = new ActionResponse<IEnumerable<MovieDto>>(){
            Message = [ "Movies fetched successfully"],
            ResultCode = 200,
            Result = movies.Select(movie => _mapper.Map<MovieDto>(movie))
        };
        return response;
    }
}
