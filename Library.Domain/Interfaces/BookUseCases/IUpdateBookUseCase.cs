using Library.Domain.Interfaces.BookUseCases.Dto;

namespace Library.Domain.Interfaces.BookUseCases {
    public interface IUpdateBookUseCase {
        Task Execute( BookUpdateDto bookInputDto );
    }

}