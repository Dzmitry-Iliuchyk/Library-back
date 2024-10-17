using FluentValidation;
using Library.Application.Auth.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.UserUseCases;
using Library.Domain.Interfaces.UserUseCases.DTO;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Implementations.UserUseCases
{

    public class RegisterUserUseCase: IRegisterUserUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<User> _validator;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ITokenService _tokenService;

        public RegisterUserUseCase( IUnitOfWork unit, IValidator<User> validator, IPasswordHasher<User> hasher, ITokenService tokenService ) {
            _unit = unit;
            _validator = validator;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponce> Execute( RegisterModel registerModel ) {
            try {
                _unit.CreateTransaction();
                var user = new User(
                    id: Guid.NewGuid(),
                    userName: registerModel.UserName,
                    email: registerModel.Email,
                    passwordHash: _hasher.HashPassword( null, registerModel.Password )
                );
                _validator.ValidateAndThrow( user );
                await _unit.userRepository.CreateAsync( user );
                await _unit.Save();
                await _unit.authRepository.AddUserToGroup( user.Id, Auth.Enums.AccessGroupEnum.User );
                await _unit.Save();
                var token = _tokenService.GenerateToken( user );
                var refreshToken = _tokenService.GenerateRefreshToken();
                await _unit.authRepository.SaveRefreshToken( user.Id, refreshToken );
                await _unit.Save();
                _unit.Commit();
                return new (token, refreshToken);
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }
        }
    }
}