using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces.BookUseCases
{
    public interface IGetFilteredBooksUseCase {
        Task<FilteredBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter );
    }
}