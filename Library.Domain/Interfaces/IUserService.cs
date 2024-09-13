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
        Task CreateUser( string userName, string email, string password );
        Task UpdateUser( Guid userId, string userName, string email ,string password  );
        Task DeleteUser( Guid userId );
    }
}
