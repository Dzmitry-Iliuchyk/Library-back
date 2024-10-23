using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.BookUseCases
{
    public interface IGetFilteredBooksUseCase {
        Task<FilteredBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter );
    }
}