using BookStore.Books.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BookStore.Books
{
    public interface IPublisherAppService :IApplicationService
    {
        Task<List<BookDto>> GetAllBookAsync();
        Task<BookOutputDto> CreateBookAsync(CreateUpdateBookDto inputDto);
        Task<BookOutputDto> UpdateBookAsync(CreateUpdateBookDto inputDto);
        Task<bool> DeleteBookAsync(Guid id);
    }
}
