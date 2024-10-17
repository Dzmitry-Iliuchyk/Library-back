using Library.Domain.Interfaces.AuthorUseCases.Dto;

namespace Library.Domain.Interfaces.AuthorUseCases
{
    public interface IGetAuthorsUseCase {
        Task<IList<AuthorDto>> Execute( int skip, int take );
    }

}
