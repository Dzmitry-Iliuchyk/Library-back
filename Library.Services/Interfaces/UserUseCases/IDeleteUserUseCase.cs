namespace Library.Application.Interfaces.UserUseCases {
    public interface IDeleteUserUseCase {
        Task Execute( Guid userId );
    }
}
