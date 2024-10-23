using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Interfaces.UserUseCases {
    public interface IGetUserBooksUseCase {
        Task<IList<TakenBookDto>> Execute( Guid userId, int skip, int take );
    }
}
