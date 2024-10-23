using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.BookUseCases {
    public interface IGetBookWithAllUseCase {
        Task<BookDto> Execute( Guid bookId );
    }
}