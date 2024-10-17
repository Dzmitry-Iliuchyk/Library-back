using AutoMapper;
using Library.Application.Auth.Enums;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.UserUseCases;
using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Application.Implementations.UserUseCases {
    public class GetUserByIdUseCase: IGetUserByIdUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetUserByIdUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<UserResponceWithAdminDto> Execute( Guid id ) {
            var userGroups = await _unit.authRepository.GetUserGroups( id );
            var isAdmin = userGroups.Contains( AccessGroupEnum.Admin );
            var userInDb = await _unit.userRepository.GetAsync( id );

            return new UserResponceWithAdminDto( userInDb.Id, userInDb.UserName, userInDb.Email, isAdmin );

        }
    }
    }