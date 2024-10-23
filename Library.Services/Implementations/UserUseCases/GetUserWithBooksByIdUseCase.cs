using AutoMapper;
using Library.Application.Auth.Enums;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Application.Interfaces.UserUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Implementations.UserUseCases {
    public class GetUserWithBooksByIdUseCase: IGetUserWithBooksByIdUseCase {
            private readonly IUnitOfWork _unit;
            private readonly IMapper _mapper;

            public GetUserWithBooksByIdUseCase( IUnitOfWork unit, IMapper mapper ) {
                _unit = unit;
                _mapper = mapper;
            }

            public async Task<UserWithBooksResponceDto> Execute( Guid id, int skip, int take ) {
                var isAdmin = ( await _unit.authRepository.GetUserGroups( id ) ).Contains( AccessGroupEnum.Admin );
                var userInDb = await _unit.userRepository.GetAsync( id );
                var books = _mapper.Map<IList<TakenBookDto>>( await _unit.userRepository.GetBooksAsync( id, skip, take ) );
                var user = new UserWithBooksResponceDto( userInDb.Id, userInDb.UserName, userInDb.Email, isAdmin, books );
                return user;
            }

        }
    }