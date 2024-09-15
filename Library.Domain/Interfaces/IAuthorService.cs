using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IAuthorService {
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthor( Guid authorId );
        Task CreateAuthor( string FirstName, string LastName, DateTime Birthday, string Country);
        Task UpdateAuthor( Guid Id, string FirstName, string LastName, DateTime Birthday, string Country );
        Task DeleteAuthor( Guid authorId );
        Task<IList<Book>> GetAllBooks( Guid authorId );
    }
}