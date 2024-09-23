using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IBookService {
        Task<IList<Book>> GetBooksAsync( int skip, int take );
        Task<Book> GetBookAsync( Guid bookId );
        Task<Book> GetBookAsync( string ISBN );
        Task<Guid> CreateBookAsync( string ISBN, string title, string genre, string description, Guid authorId );
        Task DeleteBookAsync( Guid bookId );
        Task UpdateBookAsync(Guid bookId, string ISBN, string title, string genre, string description, Guid authorId );
        Task GiveBookToClientAsync( Guid bookId, Guid clientId, int hoursToUse );
        Task FreeBookAsync( Guid bookId, Guid clientId );
        Task<(IList<Book>, int)> GetFilteredBooksAsync( int skip, int take, string authorFilter, string titleFilter );
        Task<Book> GetBookWithAllAsync( Guid bookId );
    }
}
 