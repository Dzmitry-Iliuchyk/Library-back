using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetManyAsync(int skip, int take);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IList<TakenBook>> GetBooksAsync(Guid userId, int skip, int take);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<(IList<TakenBook>, int)> GetFilteredBooksAsync(int skip, int take, string? authorFilter, string? titleFilter, Guid userId);
    }

}
