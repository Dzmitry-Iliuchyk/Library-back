namespace Library.Domain.Interfaces.UserUseCases {
    public interface IDeleteUserUseCase {
        Task Execute( Guid userId );
    }
}
