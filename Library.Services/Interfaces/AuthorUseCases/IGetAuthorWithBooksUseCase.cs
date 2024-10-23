using Library.Application.Interfaces.AuthorUseCases.Dto;

namespace Library.Application.Interfaces.AuthorUseCases
{
    public interface IGetAuthorWithBooksUseCase {
        Task<AuthorWithBooksDTO> Execute( Guid authorId );
    }

}
