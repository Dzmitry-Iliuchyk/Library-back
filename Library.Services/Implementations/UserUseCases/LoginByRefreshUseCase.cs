using Library.Application.Auth.Interfaces;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.UserUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Implementations.UserUseCases
{
    public class LoginByRefreshUseCase: ILoginByRefreshUseCase {
        private readonly IUnitOfWork _unit;
        private readonly ITokenService _tokenService;

        public LoginByRefreshUseCase( IUnitOfWork unit, ITokenService tokenService ) {
            _unit = unit;
            _tokenService = tokenService;
        }

        public async Task<AuthResponce> Execute( string accessToken, string refreshToken ) {
            var userId = _tokenService.GetUserIdFromExpiredToken( accessToken );
            var user = await _unit.userRepository.GetAsync( userId );
            var refreshTokeninDb = await _unit.authRepository.GetLastRefreshToken( userId );
            if (refreshTokeninDb.ExpiryDate < DateTime.UtcNow && refreshTokeninDb.Token != refreshToken) {
                return new( null, null);
                }
            var token = _tokenService.GenerateToken( user );
            return new( token, refreshToken );
        }
    }
}