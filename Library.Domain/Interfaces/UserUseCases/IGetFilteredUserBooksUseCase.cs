using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.UserUseCases.DTO;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface IGetFilteredUserBooksUseCase {
        Task<FilteredUserBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter, Guid userId );
    }
}
