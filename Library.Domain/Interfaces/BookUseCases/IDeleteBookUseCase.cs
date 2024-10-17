using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.BookUseCases
{
    public interface IDeleteBookUseCase {
        Task Execute( Guid bookId );
    }


}