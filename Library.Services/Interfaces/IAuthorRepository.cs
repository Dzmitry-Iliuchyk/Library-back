using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IAuthorRepository {
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthor( Guid authorId);
        Task AddAuthor( Author author);
        Task UpdateAuthor( Author changedAuthor);
        Task DeleteAuthor( Guid authorId);
        Task<IList<Book>> GetAllBooksByAuthor( Guid authorId );
    }
}
