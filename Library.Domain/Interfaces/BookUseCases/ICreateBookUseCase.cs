using Library.Domain.Interfaces.BookUseCases.Dto;

namespace Library.Domain.Interfaces.BookUseCases {
    public interface ICreateBookUseCase {
        Task<Guid> Execute( BookCreateDto createBookDto );
    }
}
