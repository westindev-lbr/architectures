import { Movie } from '../domain/movie';

export interface MovieRepository {
  getMoviesAsync(): Promise<Movie[]>;
}
