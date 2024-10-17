namespace Library.Domain.Interfaces.BookUseCases {
    public interface IFreeBookUseCase {
        Task Execute( Guid bookId, Guid clientId );
    }
}