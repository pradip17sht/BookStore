import { mapEnumToOptions } from '@abp/ng.core';

export enum BookType {
  Undefined = 0,
  Adventure = 1,
  Biography = 2,
  Dystopia = 3,
  Fantastic = 4,
  Horror = 5,
  Science = 6,
  ScienceFiction = 7,
  History = 8,
  Poetry = 9,
}

export const bookTypeOptions = mapEnumToOptions(BookType);
