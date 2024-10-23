using Library.Application.Interfaces.AuthorUseCases.Dto;

namespace Library.Application.Interfaces.AuthorUseCases {
    public interface IUpdateAuthorUseCase {
        Task Execute( AuthorDto authorDto );
    }

}
