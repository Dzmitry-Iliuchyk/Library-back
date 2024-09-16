using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces {
    public interface IUserRepository {
        Task<IList<User>> GetUsersAsync( int skip, int take );
        Task<User> GetAsync( Guid id );
        Task<User> GetAsync( string email );
        Task<IList<Book>> GetBooksAsync( Guid userId, int skip, int take );
        Task CreateUserAsync( User user );
        Task UpdateUser( User user );
        Task DeleteUserAsync( Guid userId );
    }
}
