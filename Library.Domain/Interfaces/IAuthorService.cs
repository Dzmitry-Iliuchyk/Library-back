using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IAuthorService {
        Task<IList<Author>> GetAuthorsAsync( int skip, int take );
        Task<Author> GetAuthorAsync( Guid authorId );
        Task CreateAuthorAsync( string FirstName, string LastName, DateTime Birthday, string Country);
        Task UpdateAuthorAsync( Guid Id, string FirstName, string LastName, DateTime Birthday, string Country );
        Task DeleteAuthorAsync( Guid authorId );
        Task<IList<Book>> GetBooksAsync( Guid authorId, int skip, int take );
    }
}