namespace Library.Domain.Interfaces.AuthorUseCases {
    public interface IDeleteAuthorUseCase {
        Task Execute( Guid authorId );
    }

}
