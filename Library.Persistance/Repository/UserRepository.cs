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

        public async Task<IList<TakenBook>> GetBooksAsync( Guid userId, int skip, int take ) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Include(x=>x.Books)
                .SelectMany(x=>x.Books)
                .Skip( skip )
                .Take( take )
                .ToListAsync();
            return _mapper.Map<IList<TakenBook>>( bookEntities );
        }
        public async Task<(IList<TakenBook>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter, Guid userId ) {
            var booksQuery = _dbSet
                .AsQueryable()
                .AsNoTracking()
                .Where( u => u.Id == userId )
                .Include( b => b.Books.Where( b =>
                ( string.IsNullOrEmpty( titleFilter )
                 || b.Title.Contains( titleFilter ) )
                 && ( string.IsNullOrEmpty( authorFilter )
                 || ( b.Author.FirstName.Contains( authorFilter )
                 || b.Author.LastName.Contains( authorFilter ) ) ) ) )
                .ThenInclude( b => b.Author )
                .SelectMany( x => x.Books );

            var bookCount = await booksQuery.CountAsync();

            var bookEntities = await booksQuery
                .Skip( skip )
                .Take( take )
                .ToListAsync();

            return (_mapper.Map<IList<TakenBook>>( bookEntities ), bookCount);
        }

        public async Task<User> GetAsync( Guid id ) {
            var userEntity= await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == id)
                ?? throw new UserNotFoundException();
            
            return _mapper.Map<User>( userEntity );
        }
        public async Task<User> GetAsync( string email ) {
            var userEntity= await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Email == email)
                ?? throw new UserNotFoundException();

            return _mapper.Map<User>( userEntity );
        }
    }
}
