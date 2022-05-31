import type { BookDto, BookOutputDto, CreateUpdateBookDto } from './dto/models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PublisherService {
  apiName = 'Default';

  createBook = (inputDto: CreateUpdateBookDto) =>
    this.restService.request<any, BookOutputDto>({
      method: 'POST',
      url: '/api/app/publisher/book',
      body: inputDto,
    },
    { apiName: this.apiName });

  deleteBook = (id: string) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/publisher/${id}/book`,
    },
    { apiName: this.apiName });

  getAllBook = () =>
    this.restService.request<any, BookDto[]>({
      method: 'GET',
      url: '/api/app/publisher/book',
    },
    { apiName: this.apiName });

  updateBook = (inputDto: CreateUpdateBookDto) =>
    this.restService.request<any, BookOutputDto>({
      method: 'PUT',
      url: '/api/app/publisher/book',
      body: inputDto,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
