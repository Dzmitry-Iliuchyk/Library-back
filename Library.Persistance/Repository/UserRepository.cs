using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository
{
    public sealed class UserRepository: BaseRepository<UserEntity, User>, IUserRepository {
        private readonly IMapper _mapper;
        public UserRepository( LibraryDBContext context, IMapper mapper ) : base(context){
            _mapper = mapper;
        }
        public async Task<IList<TakenBook>> GetBooksAsync( Guid userId, int skip, int take ) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Include( x => x.Books )
                .SelectMany( x => x.Books )
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
        public async Task<User> GetAsync( string email ) {
            var userEntity = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.Email == email );

            return MapToDomainEntity( userEntity );
        }

        protected override UserEntity MapToDBEntity( User domainEntity ) {
            return _mapper.Map<UserEntity>( domainEntity );
        }

        protected override User MapToDomainEntity( UserEntity dbEntity ) {
            return _mapper.Map<User>( dbEntity );
        }

        protected override IList<User> MapToDomainEntities( IList<UserEntity> dbEntities ) {
            return _mapper.Map<IList<User>>( dbEntities );
        }
    }
}
