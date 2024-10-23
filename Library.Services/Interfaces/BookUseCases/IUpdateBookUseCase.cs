using Library.Application.Interfaces.BookUseCases.Dto;

namespace Library.Application.Interfaces.BookUseCases {
    public interface IUpdateBookUseCase {
        Task Execute( BookUpdateDto bookInputDto );
    }

}