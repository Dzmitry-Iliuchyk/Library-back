using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;
using Library.Domain.Models;
using Library.Application.Exceptions;

namespace Library.Application.Implementations.BookUseCases {
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
            if (await _unit.bookRepository.Exist(updatedBook.ISBN)) {
                throw new AlreadyExistsException( $"Кника с ISBN{updatedBook.ISBN} уже существует!" );
            }
            await _unit.bookRepository.UpdateAsync( updatedBook );
            await _unit.Save();
            if (bookInputDto.Image !=null) {
                using (var stream = bookInputDto.Image.OpenReadStream()) {
                    await _image.SaveImage( stream, bookInputDto.BookId, bookInputDto.Image.ContentType.Split( "/" )[ 1 ] );
                }
            }
        }
    }
}
