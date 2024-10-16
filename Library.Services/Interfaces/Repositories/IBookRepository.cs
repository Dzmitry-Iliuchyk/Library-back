using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IList<Book>> GetBooksAsync(int skip, int take);
        Task<Book> GetBookWithAuthorAsync(Guid bookId);
        Task<Book> GetAsync(Guid bookId);
        Task<Book> GetBookWithAuthorAsync(string ISBN);
        Task CreateAsync(Book book);
        Task DeleteAsync(Book book);
        Task UpdateAsync(Book changedBook);
        Task<(IList<Book>, int)> GetFilteredBooksAsync(int skip, int take, string? authorFilter, string? titleFilter);
        Task<Book> GetBookWithAllAsync(Guid bookId);
    }
}
