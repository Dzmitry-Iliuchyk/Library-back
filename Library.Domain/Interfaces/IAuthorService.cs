using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IAuthorService {
        Task<IList<Author>> GetAllAuthors( CancellationToken token = default );
        Task<Author> GetAuthor( Guid authorId , CancellationToken token = default );
        Task CreateAuthor( string FirstName, string LastName, DateTime Birthday, string Country, CancellationToken token = default );
        Task UpdateAuthor( Guid Id, string FirstName, string LastName, DateTime Birthday, string Country, CancellationToken token = default );
        Task DeleteAuthor( Guid authorId, CancellationToken token = default );
        Task<IList<Book>> GetAllBooks( Guid authorId, CancellationToken token = default );
    }
}