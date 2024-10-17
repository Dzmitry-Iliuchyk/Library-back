using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases
{
    public class GetFilteredBooksUseCase: IGetFilteredBooksUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IImageService _image;
        public GetFilteredBooksUseCase( IUnitOfWork unit, IMapper mapper, IImageService imageService ) {
            _unit = unit;
            _mapper = mapper;
            _image = imageService;
        }

        public async Task<FilteredBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter ) {
            var result = await _unit.bookRepository.GetFilteredBooksAsync( skip, take, authorFilter, titleFilter );
            var bookResponcesDto = _mapper.Map<IList<BookResponce>>( result.Item1 );
            foreach (var item in bookResponcesDto) {
                item.Image = await _image.GetImageAsBase64( item.Id );
            }
            return new( bookResponcesDto, result.Item2);
        }

    }

    
}
/*

     public async Task<(IList<Book>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter ) {
            return await _unit.bookRepository.GetFilteredBooksAsync( skip, take, authorFilter, titleFilter );
        }

        public async Task<Book> GetBookWithAuthorAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookWithAuthorAsync( bookId );
        }
        public async Task<Book> GetBookWithAllAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookWithAllAsync( bookId );
        }

        public async Task<Book> GetBookAsync( string ISBN ) {
            return await _unit.bookRepository.GetBookWithAuthorAsync( ISBN );
        }*/