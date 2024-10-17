using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.AuthorUseCases {

    public class GetBooksByAuthorUseCase: IGetBooksByAuthorUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetBooksByAuthorUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IList<BookDto>> Execute( Guid authorId, int skip, int take ) {
            return _mapper.Map<IList<BookDto>>(await _unit.authorRepository.GetBooksByAuthorAsync( authorId, skip, take ));
        }
    }

}
