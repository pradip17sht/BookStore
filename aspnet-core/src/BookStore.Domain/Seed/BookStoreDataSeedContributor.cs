using BookStore.AppEntities;
using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Seed
{
    public class BookStoreDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public BookStoreDataSeedContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "2000",
                        Type = BookType.Science,
                        PublishDate = new DateTime(2001, 5, 7),
                        Price = 500
                    },
                    autoSave: true);

                await _bookRepository.InsertAsync(
                new Book
                {
                    Name = "Anabella 2",
                    Type = BookType.Horror,
                    PublishDate = new DateTime(2019, 1, 10),
                    Price = 800
                },
                autoSave: true);
            }
        }
    }
}
