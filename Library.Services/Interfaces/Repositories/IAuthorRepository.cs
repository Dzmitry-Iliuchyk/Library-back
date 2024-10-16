using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> GetManyAsync(int skip, int take);
        Task<Author> GetAuthorWithBooksAsync(Guid authorId);
        Task CreateAsync(Author author);
        Task UpdateAsync(Author changedAuthor);
        Task DeleteAsync(Author author);
        Task<IList<Book>> GetBooksByAuthorAsync(Guid authorId, int skip, int take);
    }
}
