using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Domain.Interfaces.AuthorUseCases {
    public interface IGetBooksByAuthorUseCase {
        Task<IList<BookDto>> Execute( Guid authorId, int skip, int take );
    }

}
