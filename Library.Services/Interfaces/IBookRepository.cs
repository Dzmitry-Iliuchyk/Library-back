using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IBookRepository {
        Task<IList<Book>> GetBooksAsync(int skip, int take);
        Task<Book> GetBookAsync( Guid bookId );
        Task<Book> GetBookAsync( string ISBN );
        Task CreateBookAsync( Book book );
        Task DeleteBookAsync( Guid bookId );
        Task UpdateBook( Book changedBook );
        Task<(IList<Book>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter );
    }
}
