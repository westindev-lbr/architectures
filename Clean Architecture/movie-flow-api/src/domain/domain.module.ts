import { Module } from '@nestjs/common';
import { Movie } from './domain/movie';

@Module({
  imports: [],
  controllers: [],
  providers: [Movie],
  exports: [Movie],
})
export class DomainModule {}
