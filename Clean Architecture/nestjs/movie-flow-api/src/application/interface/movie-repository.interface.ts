import { Movie } from '../../domain/entities/movie';

export interface MovieRepository {
  getMoviesAsync(): Promise<Movie[]>;
}
