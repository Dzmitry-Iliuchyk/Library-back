using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;
using Library.Domain.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;

namespace Library.Application.Implementations.AuthorUseCases {
    public class GetAuthorsUseCase: IGetAuthorsUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetAuthorsUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IList<AuthorDto>> Execute( int skip, int take ) {
            return _mapper.Map<IList<AuthorDto>>(await _unit.authorRepository.GetManyAsync( skip, take ));
        }
    }

}
