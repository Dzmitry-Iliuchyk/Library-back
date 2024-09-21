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
        Task<IList<TakenBook>> GetBooks(Guid userId, int skip, int take );
        Task<User> Get( Guid id );
        Task<(string, string)> Register( string userName, string email, string password );
        Task<(string, string)> Login( string email, string password );
        Task Update( Guid userId, string userName, string email ,string password  );
        Task Delete( Guid userId );
        Task<(string, string)> LoginByRefresh( string accessToken, string refreshToken );
        Task<List<string>> GetGroups( Guid id );
        Task<(IList<TakenBook>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter, Guid userId );
    }
}
