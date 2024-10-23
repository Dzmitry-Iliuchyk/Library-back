using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    public class GetBookByISBNUseCase: IGetBookByISBNUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetBookByISBNUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<BookDto> Execute( string ISBN ) {
            return _mapper.Map<BookDto>(await _unit.bookRepository.GetBookWithAuthorAsync( ISBN ));
        }
    }



}
