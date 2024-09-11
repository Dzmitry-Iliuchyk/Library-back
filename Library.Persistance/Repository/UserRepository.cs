using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository {
    public class UserRepository: IUserRepository {
        private readonly LibraryDBContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(LibraryDBContext context, IMapper mapper) {
            _dbContext = context;
            _mapper = mapper;
        }
        public async Task<IList<User>> GetAllUsers() {
            var userEntities = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IList<User>>( userEntities );
        }

        public async Task<IList<Book>> GetBooks( Guid userId ) {
            var bookEntities = await _dbContext.Users.Include(x=>x.Books).SelectMany(x=>x.Books).ToListAsync();
            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<User> GetById( Guid id ) {
            var userEntity= await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id == id);
            return _mapper.Map<User>( userEntity );
        }
    }
}
