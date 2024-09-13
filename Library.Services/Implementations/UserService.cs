using FluentValidation;
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

        public async Task CreateUser( string userName, string email, string password ) {
            var user = new User( Guid.NewGuid(), userName, email, _hasher.HashPassword( null, password ) );
            var result = _validator.Validate( user );
            if (result.IsValid) {
                await _userRepository.CreateUser( user );
            }
        }

        public async Task DeleteUser( Guid userId ) {
            await _userRepository.DeleteUser( userId );
        }

        public async Task<IList<User>> GetAllUsers() {
            return await _userRepository.GetAllUsers();
        }

        public async Task<IList<Book>> GetBooks( Guid userId ) {
            return await _userRepository.GetBooks( userId );
        }

        public async Task<User> GetById( Guid id ) {
            return await _userRepository.GetById( id );
        }

        public async Task UpdateUser( Guid userId, string userName, string email, string password ) {
            var passwordHash = _hasher.HashPassword( null, password );
            var userInDb = await _userRepository.GetById( userId );
            var passwordResult = _hasher.VerifyHashedPassword( null, userInDb.PasswordHash, passwordHash );
            if (passwordResult.HasFlag( PasswordVerificationResult.Failed )) {
                throw new Exception();
            }
            var user = new User( userId, userName, email, passwordHash );
            var result = _validator.Validate( user );
            if (result.IsValid) {
                await _userRepository.UpdateUser( user );
            }
        }
    }
}
