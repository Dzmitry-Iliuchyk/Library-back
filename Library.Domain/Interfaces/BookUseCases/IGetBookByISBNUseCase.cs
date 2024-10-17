using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces.BookUseCases {
    public interface IGetBookByISBNUseCase {
        Task<BookDto> Execute( string ISBN );

    }
}