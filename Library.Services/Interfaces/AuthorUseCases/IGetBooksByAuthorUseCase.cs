using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.AuthorUseCases {
    public interface IGetBooksByAuthorUseCase {
        Task<IList<BookDto>> Execute( Guid authorId, int skip, int take );
    }

}
