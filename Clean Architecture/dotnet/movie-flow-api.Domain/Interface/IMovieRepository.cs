using movie_flow_api.Domain.Entities;

namespace movie_flow_api.Domain.Interface;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie> GetMovieAsync(int id);
    Task<Movie> CreateMovieAsync(Movie movie);
    Task<Movie> UpdateMovieAsync(Movie movie);
    Task<Movie> DeleteMovieAsync(int id);
}
