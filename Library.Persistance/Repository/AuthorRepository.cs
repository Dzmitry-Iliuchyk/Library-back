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
    public sealed class AuthorRepository: BaseRepository<AuthorEntity, Author>, IAuthorRepository {
        private readonly IMapper _mapper;
        public AuthorRepository( LibraryDBContext context, IMapper mapper ): base(context) {
            _mapper = mapper;
        }

        public async Task<IList<Book>> GetBooksByAuthorAsync( Guid authorId, int skip, int take ) {
            var bookEntities = await _dbSet
               .AsNoTracking()
               .Include( a => a.Books )
               .Where( a => a.Id == authorId )
               .SelectMany( x => x.Books )
               .Skip( skip )
               .Take( take )
               .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<Author> GetAuthorWithBooksAsync( Guid authorId ) {
            var authorEntity = await _dbSet
                .AsNoTracking()
                .Include( x => x.Books )
                .FirstOrDefaultAsync( a => a.Id == authorId );
            return MapToDomainEntity( authorEntity );
        }

        protected override AuthorEntity MapToDBEntity( Author domainEntity ) {
            return _mapper.Map<AuthorEntity>( domainEntity );
        }

        protected override Author MapToDomainEntity( AuthorEntity dbEntity ) {
            return _mapper.Map<Author>( dbEntity );
        }

        protected override IList<Author> MapToDomainEntities( IList<AuthorEntity> dbEntities ) {
            return _mapper.Map<IList<Author>>( dbEntities );
        }
    }
}
