using Library.Application.Interfaces.BookUseCases.Dto;

namespace Library.Application.Interfaces.BookUseCases {
    public interface ICreateBookUseCase {
        Task<Guid> Execute( BookCreateDto createBookDto );
    }
}
