using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IBookService {
        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> GetBook( Guid bookId );
        Task<Book> GetBook( string ISBN );
        Task AddNewBook( Book book );
        Task DeleteBook( Guid bookId );
        Task ChangeBook( Book changedBook );
        Task GiveBookToClient( Book book, Guid clientId );
        Task FreeBook( Book book, Guid clientId );
        Task AttachImageToBook( Book book, FileStream imageStream );
    }
}