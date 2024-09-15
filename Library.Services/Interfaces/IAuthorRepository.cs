using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IAuthorRepository {
        Task<IList<Author>> GetAllAuthors( CancellationToken token = default );
        Task<Author> GetAuthor( Guid authorId, CancellationToken token = default );
        Task AddAuthor( Author author, CancellationToken token = default );
        Task UpdateAuthor( Author changedAuthor, CancellationToken token = default );
        Task DeleteAuthor( Guid authorId, CancellationToken token = default );
        Task<IList<Book>> GetAllBooksByAuthor( Guid authorId, CancellationToken token = default );
    }
}
