import { UseCase } from 'src/application/interface/use-case.interface';
import { MovieDto } from '../dto/movie.dto';
import { MovieRepository } from 'src/application/interface/movie-repository.interface';
import { Movie } from 'src/domain/entities/movie';

export class GetMoviesService implements UseCase<MovieDto[]> {
  constructor(private readonly movieRepository: MovieRepository) {}

  async execute(): Promise<MovieDto[]> {
    const movies: Movie[] = await this.movieRepository.getMoviesAsync();
    const moviesDto: Promise<MovieDto[]> = Promise.resolve(
      movies.map((movie) => ({
        id: movie.id,
        title: movie.title,
        year: movie.year,
        genre: movie.genre,
        rating: movie.rating,
        director: movie.director,
      })),
    );
    return await moviesDto;
  }
}
