using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;
using Library.Application.Exceptions;
using AutoMapper;


namespace Library.Application.Implementations.BookUseCases {
    public class CreateBookUseCase: ICreateBookUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Book> _validator;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public CreateBookUseCase( IUnitOfWork unit, IValidator<Book> validator, IImageService imageService, IMapper mapper ) {
            _unit = unit;
            _validator = validator;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<Guid> Execute( BookCreateDto createBookDto ) {
            var book = _mapper.Map<FreeBook>( createBookDto );
            
            if (await _unit.bookRepository.Exist( book.ISBN )) {
                throw new AlreadyExistsException( $"Кника с ISBN{book.ISBN} уже существует!" );
            }

            _validator.ValidateAndThrow( book );
            
            await _unit.bookRepository.CreateAsync( book );
            await _unit.Save();

            if (createBookDto.image != null) {

                using (var stream = createBookDto.image.OpenReadStream()) {
                    await _imageService.SaveImage( stream, book.Id, createBookDto.image.ContentType.Split( "/" )[1] );
                }
            }
            return book.Id;
        }
    }
}
