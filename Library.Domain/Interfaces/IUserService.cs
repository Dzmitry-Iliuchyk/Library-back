using Library.Domain.Models;
using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces {
    public interface IUserService {
        Task<IList<User>> GetUsers( int skip, int take );
        Task<IList<Book>> GetBooks(Guid userId, int skip, int take );
        Task<User> Get( Guid id );
        Task Create( string userName, string email, string password );
        Task Update( Guid userId, string userName, string email ,string password  );
        Task Delete( Guid userId );
    }
}
