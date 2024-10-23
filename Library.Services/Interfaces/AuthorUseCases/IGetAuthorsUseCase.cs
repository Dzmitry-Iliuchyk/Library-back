using Library.Application.Interfaces.AuthorUseCases.Dto;

namespace Library.Application.Interfaces.AuthorUseCases
{
    public interface IGetAuthorsUseCase {
        Task<IList<AuthorDto>> Execute( int skip, int take );
    }

}
