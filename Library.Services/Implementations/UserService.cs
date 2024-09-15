using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Implementations {
    public class UserService: IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;
        private readonly IPasswordHasher<User> _hasher;

        public UserService( IUserRepository userRepository, IValidator<User> validator, IPasswordHasher<User> hasher ) {
            this._userRepository = userRepository;
            this._validator = validator;
            this._hasher = hasher;
        }

        public async Task Create( string userName, string email, string password ) {
            var user = new User( Guid.NewGuid(), userName, email, _hasher.HashPassword( null, password ) );
            _validator.ValidateAndThrow( user );
            await _userRepository.CreateUser( user );
        }

        public async Task Delete( Guid userId ) {
            await _userRepository.DeleteUser( userId );
        }

        public async Task<IList<User>> GetAll() {
            return await _userRepository.GetAllUsers();
        }

        public async Task<IList<Book>> GetBooks( Guid userId ) {
            return await _userRepository.GetBooks( userId );
        }

        public async Task<User> Get( Guid id ) {
            return await _userRepository.GetById( id );
        }

        public async Task Update( Guid userId, string userName, string email, string password ) {
            var passwordHash = _hasher.HashPassword( null, password );
            var userInDb = await _userRepository.GetById( userId );
            var passwordResult = _hasher.VerifyHashedPassword( null, userInDb.PasswordHash, passwordHash );
            if (passwordResult.HasFlag( PasswordVerificationResult.Failed )) {
                throw new AccessDeniedException("Пароль не верный!");
            }
            var user = new User( userId, userName, email, passwordHash );
            _validator.ValidateAndThrow( user );
            await _userRepository.UpdateUser( user );
        }
    }
}
