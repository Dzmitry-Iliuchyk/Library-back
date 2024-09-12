using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IBookRepository {
        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> GetBook( Guid bookId );
        Task<Book> GetBook( string ISBN );
        Task AddNewBook( Book book );
        Task DeleteBook( Guid bookId );
        Task UpdateBook( Book changedBook );
    }
}
