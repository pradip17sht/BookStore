import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookType } from '../book-type.enum';

export interface BookDto extends AuditedEntityDto<string> {
  name?: string;
  type: BookType;
  publishDate?: string;
  price: number;
}

export interface BookOutputDto extends AuditedEntityDto<string> {
  name?: string;
  type: BookType;
  publishDate?: string;
  price: number;
}

export interface CreateUpdateBookDto extends AuditedEntityDto<string> {
  name: string;
  type: BookType;
  publishDate: string;
  price: number;
}
