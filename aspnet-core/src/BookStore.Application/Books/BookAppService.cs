using BookStore.AppEntities;
using BookStore.Books.Dto;
using BookStore.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository) : base(repository)
        {
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }

        public async Task<BookDto> FindByName(string name)
        {
            var queryable = await Repository.GetQueryableAsync();
            var result =  (from book in queryable
                          .Where(x => x.Name == name)
                           select new BookDto
                           {
                             Name =book.Name,
                             Type = book.Type,
                             PublishDate = book.PublishDate,
                             Price = book.Price
                           }).FirstOrDefault();
            return result;
        }

        public async Task<List<BookDto>> FindAllByName(string name)
        {
            var queryable = await Repository.GetQueryableAsync();
            var result = (from book in queryable
                                .Where(x => x.Name == name)
                                select new BookDto 
                                { 
                                    Name =book.Name,
                                    Type=book.Type,
                                    PublishDate=book.PublishDate,
                                    Price=book.Price
                                }).OrderBy(x => x.Name).ToList();
            return result;
        }
    }
}
