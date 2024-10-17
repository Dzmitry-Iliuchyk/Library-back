using Library.Domain.Interfaces.AuthorUseCases.Dto;

namespace Library.Domain.Interfaces.AuthorUseCases {
    public interface IUpdateAuthorUseCase {
        Task Execute( AuthorDto authorDto );
    }

}
