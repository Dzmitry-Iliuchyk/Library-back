using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces.BookUseCases {
    public interface IGetBooksUseCase {
        Task<IList<BookDto>> Execute( int skip, int take );
    }

}