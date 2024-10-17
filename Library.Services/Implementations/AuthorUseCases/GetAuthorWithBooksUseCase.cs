using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;
using Library.Domain.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;

namespace Library.Application.Implementations.AuthorUseCases {
    public class GetAuthorWithBooksUseCase: IGetAuthorWithBooksUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAuthorWithBooksUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<AuthorWithBooksDTO> Execute( Guid authorId ) {
            return _mapper.Map<AuthorWithBooksDTO>(await _unit.authorRepository.GetAuthorWithBooksAsync( authorId ));
        }
    }

}
