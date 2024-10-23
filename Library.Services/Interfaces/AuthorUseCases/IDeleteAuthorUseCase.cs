namespace Library.Application.Interfaces.AuthorUseCases {
    public interface IDeleteAuthorUseCase {
        Task Execute( Guid authorId );
    }

}
