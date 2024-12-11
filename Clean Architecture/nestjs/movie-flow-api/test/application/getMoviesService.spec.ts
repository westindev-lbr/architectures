import { Test, TestingModule } from '@nestjs/testing';
import { GetMoviesService } from '../../src/application/usecase/get-movies.service';
import { MovieRepository } from '../../src/application/interface/movie-repository.interface';
import { Category } from '../../src/domain/entities/category';

describe('Movies UseCases', () => {
  let getMoviesService: GetMoviesService;
  let mockMovieRepository: Partial<MovieRepository>;

  beforeEach(async () => {
    mockMovieRepository = {
      getMoviesAsync: jest.fn(),
    };

    const module: TestingModule = await Test.createTestingModule({
      providers: [
        GetMoviesService,
        {
          provide: 'MovieRepository',
          useValue: mockMovieRepository,
        },
      ],
    }).compile();

    getMoviesService = module.get<GetMoviesService>(GetMoviesService);
  });

  it('should be defined', () => {
    expect(getMoviesService).toBeDefined();
  });

  it('should return an empty list if there are no movies', async () => {
    // Arrange
    (mockMovieRepository.getMoviesAsync as jest.Mock).mockResolvedValue([]);
    // Act
    const movies = await getMoviesService.execute();
    // Assert
    expect(movies).toEqual([]);
  });

  it('should return a list of moviesDto if there are movies', async () => {
    // Arrange
    (mockMovieRepository.getMoviesAsync as jest.Mock).mockResolvedValue([
      {
        id: 1,
        title: 'The Matrix',
        year: 1999,
        genre: [Category.ACTION, Category.SCI_FI],
        director: ['Lana Wachowski', 'Lilly Wachowski'],
        rating: 5,
      },
      {
        id: 2,
        title: 'Alien',
        year: 1979,
        genre: [Category.HORROR, Category.SCI_FI],
        director: ['Ridley Scott'],
        rating: 4,
      },
    ]);

    // Act
    const movies = await getMoviesService.execute();

    expect(movies).toEqual([
      {
        id: 1,
        title: 'The Matrix',
        year: 1999,
        genre: ['Action', 'Sci-Fi'],
        director: ['Lana Wachowski', 'Lilly Wachowski'],
        rating: 5,
      },
      {
        id: 2,
        title: 'Alien',
        year: 1979,
        genre: ['Horror', 'Sci-Fi'],
        director: ['Ridley Scott'],
        rating: 4,
      },
    ]);
  });
});
