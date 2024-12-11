import { Injectable } from '@nestjs/common';
import { Movie } from 'src/domain/entities/movie';
import { MovieRepository } from 'src/application/interface/movie-repository.interface';

@Injectable()
export class MovieRepositoryImplService implements MovieRepository {
  async getMoviesAsync(): Promise<Movie[]> {
    return [];
  }
}
