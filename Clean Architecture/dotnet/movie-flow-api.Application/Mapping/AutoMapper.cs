using AutoMapper;
using movie_flow_api.Application.Dto;
using movie_flow_api.Domain.Entities;

namespace movie_flow_api.Application.Mapping;

public class Automapper : Profile
{
    public Automapper()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}