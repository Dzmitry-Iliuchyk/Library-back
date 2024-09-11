using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Interfaces {
    public interface IAuthorRepository {
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthor( int authorId );
        Task AddAuthor( Author author );
        Task ChangeAuthor( int authorId, Author changedAuthor );
        Task DeleteAuthor( int authorId );
        Task<IList<Book>> GetAllBooks( int authorId );
    }
}
