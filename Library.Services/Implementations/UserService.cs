using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Implementations {
    public class UserService: IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;

        public UserService( IUserRepository userRepository, IValidator<User> validator ) {
            this._userRepository = userRepository;
            this._validator = validator;
        }

        public async Task CreateUser( User user ) {
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

        public async Task UpdateUser( User user ) {
            var result = _validator.Validate( user );
            if (result.IsValid) {
                await _userRepository.UpdateUser( user );
            }
        }
    }
}
