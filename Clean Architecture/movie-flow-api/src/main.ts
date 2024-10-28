import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { NestFastifyApplication } from '@nestjs/platform-fastify';
import { log } from 'console';

async function bootstrap() {
  const app = await NestFactory.create<NestFastifyApplication>(AppModule);
  await app.listen({ port: 3000 });
  app.useLogger(['log', 'error', 'debug', 'verbose']);
  log(`Application is running on : ${await app.getUrl()}`);
}
bootstrap();
