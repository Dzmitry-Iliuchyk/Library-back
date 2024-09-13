using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IBookService {
        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> GetBook( Guid bookId );
        Task<Book> GetBook( string ISBN );
        Task AddNewBook( string ISBN, string title, string genre, string description, Guid authorId );
        Task DeleteBook( Guid bookId );
        Task UpdateBook(Guid bookId, string ISBN, string title, string genre, string description, Guid authorId );
        Task GiveBookToClient( Guid bookId, Guid clientId, TimeSpan periodToUse );
        Task FreeBook( Guid bookId, Guid clientId );
        Task AttachImageToBook( Guid bookId, FileStream imageStream );
        Task GetImageToBook( Guid bookId );
    }
}
 