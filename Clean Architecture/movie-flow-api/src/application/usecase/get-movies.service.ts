import { Inject, Injectable } from '@nestjs/common';
import { UseCase } from 'src/domain/interface/use-case.interface';
import { MovieDto } from '../dto/movie.dto';
import { MovieRepository } from 'src/domain/interface/movie-repository.interface';
import { Movie } from 'src/domain/domain/movie';

@Injectable()
export class GetMoviesService implements UseCase<MovieDto[]> {
  constructor(
    @Inject('MovieRepository')
    private readonly movieRepository: MovieRepository,
  ) {}

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
