using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
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


        public async Task UpdateUser( User updatedUser ) {
            var userEntity = _mapper.Map<UserEntity>( updatedUser );
            _dbContext.Users.Update( userEntity );
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateUser( User user ) {
            var userEntity = _mapper.Map<UserEntity>( user );
            await _dbContext.Users.AddAsync( userEntity );
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser( Guid userId ) {
            var userToDelete = await _dbContext.Users.FirstOrDefaultAsync()
                ?? throw new UserNotFoundException();
            _dbContext.Users.Remove( userToDelete );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAllUsers() {
            var userEntities = await _dbContext.Users.AsNoTracking().ToListAsync();
            return _mapper.Map<IList<User>>( userEntities );
        }

        public async Task<IList<Book>> GetBooks( Guid userId ) {
            var bookEntities = await _dbContext.Users.AsNoTracking().Include(x=>x.Books).SelectMany(x=>x.Books).ToListAsync();
            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<User> GetById( Guid id ) {
            var userEntity= await _dbContext.Users.AsNoTracking().Include( x => x.Books ).FirstOrDefaultAsync(x=>x.Id == id);
            return _mapper.Map<User>( userEntity );
        }
    }
}
