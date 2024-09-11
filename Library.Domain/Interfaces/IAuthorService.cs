using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IAuthorService {
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthor( int authorId );
        Task AddAuthor( Author author );
        Task ChangeAuthor( int authorId, Author changedAuthor );
        Task DeleteAuthor( int authorId );
        Task<IList<Book>> GetAllBooks( int authorId );
    }
}