import { Category } from './category';

export class Movie {
  constructor(
    public id: number,
    public title: string,
    public year: number,
    public director: string[],
    public genre: Category[],
    public rating: number,
  ) {}
}
