using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Repository
{
    public sealed class BookRepository: BaseRepository<BookEntity, Book>, IBookRepository {
        private readonly IMapper _mapper;
        public BookRepository( LibraryDBContext dbContext, IMapper mapper ): base(dbContext) {
            this._mapper = mapper;
        }
        public async Task<IList<Book>> GetBooksAsync(int skip, int take) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return MapToDomainEntities( bookEntities );
        }
        public async Task<(IList<Book>,int)> GetFilteredBooksAsync(int skip, int take, string? authorFilter, string? titleFilter) {
            var bookQuery =  _dbSet
                .AsQueryable()
                .AsNoTracking()
                .Include( b => b.Author )
                 .Where( b => ( string.IsNullOrEmpty( titleFilter )
                 || b.Title.Contains( titleFilter ) )
                 && ( string.IsNullOrEmpty( authorFilter )
                 || ( b.Author.FirstName.Contains( authorFilter )
                 || b.Author.LastName.Contains( authorFilter ) ) ) );

            var bookCount = await bookQuery.CountAsync();

            var bookEntities = await bookQuery
                .Skip( skip )
                .Take( take )
                .ToListAsync();

            return (MapToDomainEntities( bookEntities ), bookCount);
        }

        public async Task<Book> GetBookWithAuthorAsync( Guid bookId ) {
            var bookEntity = await _dbSet
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.Id == bookId );
            return MapToDomainEntity( bookEntity );
        }
        public async Task<Book> GetBookWithAllAsync( Guid bookId ) {
            var bookEntity = await _dbSet
                .AsNoTracking()
                .Include(b=>b.Author)
                .Include(b=>b.User)
                .FirstOrDefaultAsync( b => b.Id == bookId );
            return MapToDomainEntity( bookEntity );
        }

        public async Task<Book> GetBookWithAuthorAsync( string ISBN ) {
            var bookEntity = await _dbSet
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.ISBN == ISBN );
            return MapToDomainEntity( bookEntity );
        }
        protected override BookEntity MapToDBEntity( Book domainEntity ) {
            return _mapper.Map<BookEntity>( domainEntity );
        }

        protected override Book MapToDomainEntity( BookEntity dbEntity ) {
            return _mapper.Map<Book>( dbEntity );
        }

        protected override IList<Book> MapToDomainEntities( IList<BookEntity> dbEntities ) {
            return _mapper.Map<IList<Book>>( dbEntities );
        }

        public async Task<bool> Exist( string isbn ) {
            return await _dbSet.AsNoTracking().AnyAsync( x => x.ISBN == isbn );
        }
    }
}
