using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    public class GetBooksUseCase: IGetBooksUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetBooksUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IList<BookDto>> Execute( int skip, int take ) {
            return _mapper.Map<IList<BookDto>>(await _unit.bookRepository.GetBooksAsync( skip, take ));
        }
    }
}
