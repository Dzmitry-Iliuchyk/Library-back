using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.UserUseCases;
using Library.Domain.Interfaces.UserUseCases.DTO;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Implementations.UserUseCases
{
    public class UpdateUserUseCase: IUpdateUserUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<User> _validator;
        private readonly IPasswordHasher<User> _hasher;

        public UpdateUserUseCase( IUnitOfWork unit, IValidator<User> validator, IPasswordHasher<User> hasher ) {
            _unit = unit;
            _validator = validator;
            _hasher = hasher;
        }

        public async Task Execute( UserDto userDto) {
            var userInDb = await _unit.userRepository.GetAsync( userDto.Id );
            var result = _hasher.VerifyHashedPassword( null, userInDb.PasswordHash, userDto.Password );
            if (result == PasswordVerificationResult.Failed) {
                throw new InvalidPasswordException( "Пароль не подходит!" );
            }
            var user = new User( userDto.Id, userDto.UserName, userDto.Email, userInDb.PasswordHash );
            _validator.ValidateAndThrow( user );
            await _unit.userRepository.UpdateAsync( user );
            await _unit.Save();
        }
    }
}