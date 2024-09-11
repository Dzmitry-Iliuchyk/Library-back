using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces {
    public interface IBookService {
        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> GetBook( int bookId );
        Task<Book> GetBook( string ISBN );
        Task AddNewBook( Book book );
        Task DeleteBook( int bookId );
        Task ChangeBook( int bookId, Book changedBook );
        Task GiveBookToClient( Book book, int clientId );
        Task FreeBook( Book book, int clientId );
        Task AttachImageToBook( Book book, FileStream imageStream );
    }
}