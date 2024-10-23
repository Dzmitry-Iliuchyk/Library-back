
using Library.Application.Interfaces.AuthorUseCases.Dto;

namespace Library.Application.Interfaces.AuthorUseCases
{
    public interface ICreateAuthorUseCase {
        Task Execute( CreateAuthorDTO authorDTO );
    }

}
