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
        private readonly DbSet<UserEntity> _dbSet;
        private readonly IMapper _mapper;
        public UserRepository(LibraryDBContext context, IMapper mapper) {
            _dbSet = context.Users;
            _mapper = mapper;
        }

        public Task UpdateUser( User updatedUser ) {
            var userEntity = _mapper.Map<UserEntity>( updatedUser );
            _dbSet.Update( userEntity );
            return Task.CompletedTask;

        }
        public async Task CreateUserAsync( User user ) {
            var userEntity = _mapper.Map<UserEntity>( user );
            await _dbSet.AddAsync( userEntity );
        }

        public async Task DeleteUserAsync( Guid userId ) {
            var userToDelete = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new UserNotFoundException();
            _dbSet.Remove( userToDelete );

        }

        public async Task<IList<User>> GetUsersAsync( int skip, int take ) {
            var userEntities = await _dbSet
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return _mapper.Map<IList<User>>( userEntities );
        }

        public async Task<IList<Book>> GetBooksAsync( Guid userId, int skip, int take ) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Include(x=>x.Books)
                .Skip( skip )
                .Take( take )
                .SelectMany(x=>x.Books)
                .ToListAsync();
            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<User> GetByIdAsync( Guid id ) {
            var userEntity= await _dbSet
                .AsNoTracking()
                .Include( x => x.Books )
                .FirstOrDefaultAsync(x=>x.Id == id);
            return _mapper.Map<User>( userEntity );
        }
    }
}
