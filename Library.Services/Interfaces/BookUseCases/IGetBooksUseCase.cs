using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.BookUseCases {
    public interface IGetBooksUseCase {
        Task<IList<BookDto>> Execute( int skip, int take );
    }

}