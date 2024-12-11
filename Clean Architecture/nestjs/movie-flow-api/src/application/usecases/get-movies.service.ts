import { Movie } from 'src/domain/entities/movie';
import { MovieRepository } from '../interface/movie-repository.interface';
import { UseCase } from '../interface/use-case.interface';

export class GetMoviesService implements UseCase<Movie[]> {
  constructor(private readonly movieRepository: MovieRepository) {}

  async execute(): Promise<Movie[]> {
    return await this.movieRepository.getMoviesAsync();
  }
}
