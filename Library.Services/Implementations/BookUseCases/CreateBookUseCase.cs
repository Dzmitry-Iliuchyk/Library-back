using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;


namespace Library.Application.Implementations.BookUseCases {
    public class CreateBookUseCase: ICreateBookUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Book> _validator;
        private readonly IImageService _imageService;

        public CreateBookUseCase( IUnitOfWork unit, IValidator<Book> validator, IImageService imageService ) {
            _unit = unit;
            _validator = validator;
            _imageService = imageService;
        }

        public async Task<Guid> Execute( BookCreateDto createBookDto ) {
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: createBookDto.ISBN,
                title: createBookDto.title,
                genre: createBookDto.genre,
                description: createBookDto.description,
                authorId: createBookDto.authorId );
            if (createBookDto.image != null) {

                using (var stream = createBookDto.image.OpenReadStream()) {
                    await _imageService.SaveImage( stream, book.Id );
                }
            }
            _validator.ValidateAndThrow( book );
            await _unit.bookRepository.CreateAsync( book );
            await _unit.Save();
            return book.Id;
        }
    }
}
