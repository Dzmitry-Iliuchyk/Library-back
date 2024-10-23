using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface IGetFilteredUserBooksUseCase {
        Task<FilteredUserBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter, Guid userId );
    }
}
