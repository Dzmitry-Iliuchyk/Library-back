
namespace Library.Application.Interfaces.BookUseCases
{
    public interface IDeleteBookUseCase {
        Task Execute( Guid bookId );
    }
}