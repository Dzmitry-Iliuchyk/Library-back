using Library.Application.Auth.Interfaces;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.UserUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Implementations.UserUseCases
{
    public class LoginUserUseCase: ILoginUserUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ITokenService _tokenService;

        public LoginUserUseCase( IUnitOfWork unit, IPasswordHasher<User> hasher, ITokenService tokenService ) {
            _unit = unit;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponce> Execute( LoginModel loginModel ) {
            var user = await _unit.userRepository.GetAsync( loginModel.email );
            var result = _hasher.VerifyHashedPassword( null, user.PasswordHash, loginModel.password );
            if (result == PasswordVerificationResult.Failed) {
                throw new InvalidPasswordException( "Пароль не подходит!" );
            }
            var token = _tokenService.GenerateToken( user );
            await _unit.authRepository.RemoveAllRefreshTokens( user.Id );
            await _unit.Save();
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _unit.authRepository.SaveRefreshToken( user.Id, refreshToken );
            await _unit.Save();
            return new(token, refreshToken);
        }
    }
}