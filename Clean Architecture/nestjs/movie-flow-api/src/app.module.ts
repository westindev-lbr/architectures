import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { InfrastructureModule } from './infrastructure/infrastructure.module';
import { ApiModule } from './api/api.module';
import { AppService } from './app.service';

@Module({
  imports: [InfrastructureModule, ApiModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
