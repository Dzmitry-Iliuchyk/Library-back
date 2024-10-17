using FluentValidation;
using Library.Application.Auth.Interfaces;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Implementations {
    public class UserService: IUserService {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<User> _validator;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ITokenService _tokenService;

        public UserService( IUnitOfWork unitOfWork, IValidator<User> validator, IPasswordHasher<User> hasher, ITokenService tokenService ) {
            this._unit = unitOfWork;
            this._validator = validator;
            this._hasher = hasher;
            this._tokenService = tokenService;
        }


        /// <returns>First parameter is AccessToken, second is RefreshToken</returns>
        public async Task<(string, string)> Register( string userName, string email, string password ) {
            try {
                _unit.CreateTransaction();
                var user = new User( id: Guid.NewGuid(),
                    userName: userName,
                    email: email,
                    passwordHash: _hasher.HashPassword( null, password ) );
                _validator.ValidateAndThrow( user );
                await _unit.userRepository.CreateAsync( user );
                await _unit.Save();
                await _unit.authRepository.AddUserToGroup( user.Id, Auth.Enums.AccessGroupEnum.User );
                await _unit.Save();
                var token = _tokenService.GenerateToken( user );
                var refreshToken = _tokenService.GenerateRefreshToken();
                await _unit.authRepository.SaveRefreshToken( userId: user.Id, refreshToken );
                await _unit.Save();
                _unit.Commit();
                return (token, refreshToken);
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }

        }
        /// <returns>First parameter is AccessToken, second is RefreshToken</returns>
        public async Task<(string, string)> Login( string email, string password ) {
            var user = await _unit.userRepository.GetAsync( email );

            var result = _hasher.VerifyHashedPassword( null, user.PasswordHash, password );

            if (result == PasswordVerificationResult.Failed) {
                throw new InvalidPasswordException( "Пароль не подходит!" );
            }
            var token = _tokenService.GenerateToken( user );
            await _unit.authRepository.RemoveAllRefreshTokens( userId: user.Id );
            await _unit.Save();
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _unit.authRepository.SaveRefreshToken( userId: user.Id, refreshToken );
            await _unit.Save();
            return (token, refreshToken);
        }
        /// <returns>First parameter is AccessToken, second is RefreshToken</returns>
        public async Task<(string, string)> LoginByRefresh( string accessToken, string refreshToken ) {
            var userId = _tokenService.GetUserIdFromExpiredToken( accessToken );
            var user = await _unit.userRepository.GetAsync( userId );
            var refreshTokeninDb = await _unit.authRepository.GetLastRefreshToken( userId );
            if (refreshTokeninDb.ExpiryDate < DateTime.UtcNow && refreshTokeninDb.Token != refreshToken) {
                throw new InvalidTokenException( "Неверный токен!" );
            }
            var token = _tokenService.GenerateToken( user );

            return (token, refreshToken);
        }

        public async Task Delete( Guid userId ) {
            var user = await _unit.userRepository.GetAsync( userId );
            await _unit.userRepository.DeleteAsync( user );
            await _unit.Save();
        }

        public async Task<IList<User>> GetUsers( int skip, int take ) {
            return await _unit.userRepository.GetManyAsync( skip, take );
        }
        public async Task<(IList<TakenBook>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter, Guid userId ) {
            return await _unit.userRepository.GetFilteredBooksAsync( skip, take, authorFilter, titleFilter, userId );
        }
        public async Task<IList<TakenBook>> GetBooks( Guid userId, int skip, int take ) {
            return await _unit.userRepository.GetBooksAsync( userId, skip, take );
        }

        public async Task<User> Get( Guid id ) {
            return await _unit.userRepository.GetAsync( id );
        }
        public async Task<List<string>> GetGroups( Guid id ) {
            var groups = await _unit.authRepository.GetUserGroups( id );
            return groups.Select( x => x.ToString() ).ToList();
        }

        public async Task Update( Guid userId, string userName, string email, string password ) {

            var userInDb = await _unit.userRepository.GetAsync( userId );
            var result = _hasher.VerifyHashedPassword( null, userInDb.PasswordHash, password );

            if (result == PasswordVerificationResult.Failed) {
                throw new InvalidPasswordException( "Пароль не подходит!" );
            }
            var user = new User( id: userId,
                userName: userName,
                email: email,
                passwordHash: userInDb.PasswordHash );
            _validator.ValidateAndThrow( user );
            await _unit.userRepository.UpdateAsync( user );
            await _unit.Save();

        }

    }
}
