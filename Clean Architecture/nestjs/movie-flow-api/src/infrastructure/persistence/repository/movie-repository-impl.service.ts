import { Injectable } from '@nestjs/common';
import { Movie } from 'src/domain/domain/movie';
import { MovieRepository } from 'src/domain/interface/movie-repository.interface';

@Injectable()
export class MovieRepositoryImplService implements MovieRepository {
  async getMoviesAsync(): Promise<Movie[]> {
    return [];
  }
}
