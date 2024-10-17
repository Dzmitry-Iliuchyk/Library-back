using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    // Класс для обновления книги
    public class UpdateBookUseCase: IUpdateBookUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Book> _validator;

        public UpdateBookUseCase( IUnitOfWork unit, IValidator<Book> validator ) {
            _unit = unit;
            _validator = validator;
        }

        public async Task Execute( BookUpdateDto bookInputDto  ) {
            Book updatedBook;
            var bookInDb = await _unit.bookRepository.GetBookWithAuthorAsync( bookInputDto.BookId );
            if (bookInDb is TakenBook taken) {
                updatedBook = new TakenBook(
                    id: bookInputDto.BookId,
                    client_id: taken.ClientId,
                    ISBN: bookInputDto.ISBN,
                    title: bookInputDto.Title,
                    genre: bookInputDto.Genre,
                    description: bookInputDto.Description,
                    authorId: bookInputDto.AuthorId,
                    takenAt: taken.TakenAt,
                    returnTo: taken.ReturnTo );
            } else {
                updatedBook = new FreeBook(
                    id: bookInputDto.BookId,
                    ISBN: bookInputDto.ISBN,
                    title: bookInputDto.Title,
                    genre: bookInputDto.Genre,
                    description: bookInputDto.Description,
                    authorId: bookInputDto.AuthorId );
            }
            _validator.ValidateAndThrow( updatedBook );
            await _unit.bookRepository.UpdateAsync( updatedBook );
            await _unit.Save();
        }
    }
}
