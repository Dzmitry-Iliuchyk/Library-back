namespace Library.Application.Interfaces.BookUseCases {
    public interface IFreeBookUseCase {
        Task Execute( Guid bookId, Guid clientId );
    }
}