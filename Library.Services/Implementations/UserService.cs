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

        public UserService( IUserRepository userRepository ) {
            this._userRepository = userRepository;
        }

        public Task<IList<User>> GetAllUsers() {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetBooks( Guid userId ) {
            throw new NotImplementedException();
        }
    }
}
