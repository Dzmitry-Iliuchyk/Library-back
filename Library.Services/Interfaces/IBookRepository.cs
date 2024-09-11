using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Interfaces {
    public interface IBookRepository {
        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> GetBook( int bookId );
        Task<Book> GetBook( string ISBN );
        Task AddNewBook( Book book );
        Task DeleteBook( int bookId );
        Task UpdateBook( int bookId, Book changedBook );
    }
}
