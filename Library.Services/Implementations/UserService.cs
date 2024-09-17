using FluentValidation;
using Library.Application.Auth.Interfaces;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
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

        public async Task<string> Register( string userName, string email, string password ) {
            try {
                _unit.CreateTransaction();
                var user = new User( id:Guid.NewGuid(),
                    userName: userName,
                    email: email,
                    passwordHash: _hasher.HashPassword( null, password ) );
                _validator.ValidateAndThrow( user );
                await _unit.userRepository.CreateUserAsync( user );
                await _unit.Save();
                await _unit.authRepository.AddUserToGroup( user, Auth.Enums.AccessGroupEnum.User );
                await _unit.Save();
                _unit.Commit();
                var token = _tokenService.GenerateToken( user );
                return token; 
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }
            
        }

        public async Task<string> Login( string email, string password ) {
            var user = await _unit.userRepository.GetAsync( email );

            var result = _hasher.VerifyHashedPassword( null, user.PasswordHash, password );

            if (result == PasswordVerificationResult.Failed) {
                throw new InvalidPasswordException("Пароль не подходит!");
            }
            var token = _tokenService.GenerateToken( user );
            return token;
        }

        public async Task Delete( Guid userId ) {
            await _unit.userRepository.DeleteUserAsync( userId );
            await _unit.Save();
        }

        public async Task<IList<User>> GetUsers( int skip, int take ) {
            return await _unit.userRepository.GetUsersAsync( skip, take );
        }

        public async Task<IList<Book>> GetBooks( Guid userId, int skip, int take ) {
            return await _unit.userRepository.GetBooksAsync( userId, skip, take );
        }

        public async Task<User> Get( Guid id ) {
            return await _unit.userRepository.GetAsync( id );
        }

        public async Task Update( Guid userId, string userName, string email, string password ) {
            try {
                _unit.CreateTransaction();
                var passwordHash = _hasher.HashPassword( null, password );
                var userInDb = await _unit.userRepository.GetAsync( userId );
                var passwordResult = _hasher.VerifyHashedPassword( null, userInDb.PasswordHash, passwordHash );
                if (passwordResult.HasFlag( PasswordVerificationResult.Failed )) {
                    throw new AccessDeniedException( "Пароль не верный!" );
                }
                var user = new User(id: userId,
                    userName: userName,
                    email: email,
                    passwordHash: passwordHash );
                _validator.ValidateAndThrow( user );
                await _unit.userRepository.UpdateUser( user );
                await _unit.Save();
                _unit.Commit();
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }
        }
    }
}
