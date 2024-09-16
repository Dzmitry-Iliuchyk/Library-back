using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IAuthorRepository {
        Task<IList<Author>> GetAuthorsAsync( int skip, int take );
        Task<Author> GetAuthorAsync( Guid authorId);
        Task AddAuthorAsync( Author author);
        Task UpdateAuthorAsync( Author changedAuthor);
        Task DeleteAuthorAsync( Guid authorId);
        Task<IList<Book>> GetBooksByAuthorAsync( Guid authorId, int skip, int take );
    }
}
