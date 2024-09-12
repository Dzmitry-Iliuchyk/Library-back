using Library.Domain.Models;
using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces {
    public interface IUserService {
        Task<IList<User>> GetAllUsers();
        Task<IList<Book>> GetBooks(Guid userId);
        Task<User> GetById( Guid id );
        Task CreateUser( User user );
        Task UpdateUser( User user );
        Task DeleteUser( Guid userId );
    }
}
