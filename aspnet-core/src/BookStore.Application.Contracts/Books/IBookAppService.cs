using BookStore.Books.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books
{
    public interface IBookAppService : ICrudAppService< //Define CRUD methods
                     BookDto, //Used to show books
                     Guid, //Primary key of the book entity
                     PagedAndSortedResultRequestDto, //Used for paging/sorting
                     CreateUpdateBookDto> //Used to create/update a book
    {
    }
}
