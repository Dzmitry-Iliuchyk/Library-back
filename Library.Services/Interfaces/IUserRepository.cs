using Library.Domain.Models.Book;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IUserRepository {
        Task<IList<User>> GetAllUsers();
        Task<User> GetById(Guid id);
        Task<IList<Book>> GetBooks( Guid userId );
        Task CreateUser( User user );
        Task UpdateUser( User user );
        Task DeleteUser( Guid userId );
    }
}
