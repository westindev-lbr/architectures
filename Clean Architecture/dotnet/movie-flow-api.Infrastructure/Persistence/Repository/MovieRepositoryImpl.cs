using System.Data;
using Dapper;
using movie_flow_api.Domain.Entities;
using movie_flow_api.Domain.Interface;

namespace movie_flow_api.Infrastructure.Persistence.Repository;

public class MovieRepositoryImpl(IDbConnection dbConnection) : IMovieRepository
{
    public Task<Movie> CreateMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> GetMovieAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        var query = 
            @"SELECT
                id as Id,
                title as Title,
                year as Year,
                image as ImageUrl,
                rating as Rating 
            FROM 
                movie";
        var movies = await dbConnection.QueryAsync<Movie>(query);
        return movies;
    }

    public Task<Movie> UpdateMovieAsync(Movie movie)
    {
        throw new NotImplementedException();
    }
} 