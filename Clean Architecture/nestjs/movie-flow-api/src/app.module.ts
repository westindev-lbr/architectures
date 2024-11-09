import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainModule } from './domain/domain.module';
import { ApplicationModule } from './application/application.module';
import { InfrastructureModule } from './infrastructure/infrastructure.module';
import { ApiModule } from './api/api.module';

@Module({
  imports: [DomainModule, ApplicationModule, InfrastructureModule, ApiModule],
  controllers: [],
  providers: [],
})
export class AppModule {}
