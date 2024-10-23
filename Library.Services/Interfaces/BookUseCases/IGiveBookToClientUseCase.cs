namespace Library.Application.Interfaces.BookUseCases {

    public interface IGiveBookToClientUseCase {
        Task Execute( Guid bookId, Guid clientId, int hoursToUse );

    }
}
