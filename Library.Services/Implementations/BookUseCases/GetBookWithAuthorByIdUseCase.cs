using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    public class GetBookWithAuthorByIdUseCase: IGetBookWithAuthorByIdUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetBookWithAuthorByIdUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<BookResponce> Execute( Guid bookId ) {
            return _mapper.Map<BookResponce>(await _unit.bookRepository.GetBookWithAuthorAsync( bookId ));
        }
    }



}
