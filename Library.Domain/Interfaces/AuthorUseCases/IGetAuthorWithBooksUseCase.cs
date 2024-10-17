using Library.Domain.Interfaces.AuthorUseCases.Dto;

namespace Library.Domain.Interfaces.AuthorUseCases
{
    public interface IGetAuthorWithBooksUseCase {
        Task<AuthorWithBooksDTO> Execute( Guid authorId );
    }

}
