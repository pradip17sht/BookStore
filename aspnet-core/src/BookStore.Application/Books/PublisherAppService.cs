using BookStore.AppEntities;
using BookStore.Books.Dto;
using BookStore.Permissions;
using BookStore.Results;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    [Authorize(BookStorePermissions.Books.List)]
    public class PublisherAppService : ApplicationService, IPublisherAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public PublisherAppService(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Get All list of Books 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<List<BookDto>> GetAllBookAsync()
        {
            try
            {
                var books = await _bookRepository.GetListAsync();
                var mappedBooks = ObjectMapper.Map<List<Book>, List<BookDto>>(books);
                return mappedBooks;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }


        /// <summary>
        /// Create Book and save to database in book table
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<BookOutputDto> CreateBookAsync(CreateUpdateBookDto inputDto)
        {
            var validateInput = ValidateInput(inputDto);
            if (validateInput.Status == false)
                throw new UserFriendlyException(validateInput.Message, "403");
            try
            {
                var _book = new Book
                {
                    Name = inputDto.Name,
                    Type = inputDto.Type,
                    PublishDate = inputDto.PublishDate,
                    Price = inputDto.Price,
                };
                await _bookRepository.InsertAsync(_book);
                return new BookOutputDto()
                {
                    Name = inputDto.Name,
                    Type = inputDto.Type,
                    PublishDate = inputDto.PublishDate,
                    Price = inputDto.Price
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }


        /// <summary>
        /// Edit Book and update to database in book table
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<BookOutputDto> UpdateBookAsync(CreateUpdateBookDto inputDto)
        {
            var validateInput = ValidateInput(inputDto);
            if (validateInput.Status == false)
                throw new UserFriendlyException(validateInput.Message, "403");

            var existingBook = await _bookRepository.FirstOrDefaultAsync(x => x.Id == inputDto.Id);
            try
            {
                existingBook.Name = inputDto.Name;
                existingBook.Type = inputDto.Type;
                existingBook.PublishDate = inputDto.PublishDate;
                existingBook.Price = inputDto.Price;

                await _bookRepository.UpdateAsync(existingBook, true);

                return new BookOutputDto()
                {
                    Name = inputDto.Name,
                    Type = inputDto.Type,
                    PublishDate = inputDto.PublishDate,
                    Price = inputDto.Price
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }


        /// <summary>
        /// Delete all the Book from book table
        /// </summary>
        /// <param Id="id">Updated Id</param>
        /// <returns>True if succeeded, false otherwise</returns>
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            try
            {
                var book = await _bookRepository.FirstOrDefaultAsync(p => p.Id == id);
                if (book == null)
                    throw new UserFriendlyException("No Book found.", "403");

                await _bookRepository.DeleteAsync(book, true);

                return true;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// Validates all the properties of <paramref name="inputDto"/>.
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns>A <see cref="ValidatorDto"/> object.</returns>
        public static ValidatorDto ValidateInput(CreateUpdateBookDto inputDto)
        {
            if (string.IsNullOrEmpty(inputDto.Name))
            {
                return new ValidatorDto(false, "Name is required.");
            }
            else
            {
                return new ValidatorDto(true, "Valid");
            }
        }
    }
}
