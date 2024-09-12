using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IAuthorService {
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthor( Guid authorId );
        Task AddAuthor( Author author );
        Task ChangeAuthor( Author changedAuthor );
        Task DeleteAuthor( Guid authorId );
        Task<IList<Book>> GetAllBooks( Guid authorId );
    }
}