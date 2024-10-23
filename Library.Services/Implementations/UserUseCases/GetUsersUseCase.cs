using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.UserUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Implementations.UserUseCases
{
    public class GetUsersUseCase: IGetUsersUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public GetUsersUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IList<UserResponceDto>> Execute( int skip, int take ) {
            var users = await _unit.userRepository.GetManyAsync( skip, take );
            return _mapper.Map<IList<UserResponceDto>>( users );
        }
    }
}