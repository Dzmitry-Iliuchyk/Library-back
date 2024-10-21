using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    // Класс для обновления книги
    public class UpdateBookUseCase: IUpdateBookUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Book> _validator;
        private readonly IImageService _image;

        public UpdateBookUseCase( IUnitOfWork unit, IValidator<Book> validator, IImageService image ) {
            _unit = unit;
            _validator = validator;
            _image = image; 
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
            if (bookInputDto.Image !=null) {
                using (var stream = bookInputDto.Image.OpenReadStream()) {
                    _image.SaveImage( stream, bookInputDto.BookId, bookInputDto.Image.ContentType.Split( "/" )[ 1 ] );
                }
            }
        }
    }
}
