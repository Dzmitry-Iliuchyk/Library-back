using Library.Domain.Interfaces.AuthorUseCases.Dto;

namespace Library.Domain.Interfaces.AuthorUseCases
{
    public interface ICreateAuthorUseCase {
        Task Execute( CreateAuthorDTO authorDTO );
    }

}
