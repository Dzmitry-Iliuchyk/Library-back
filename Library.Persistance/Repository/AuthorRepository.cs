using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository {
    public class AuthorRepository: IAuthorRepository {
        private readonly DbSet<AuthorEntity> _dbset;
        private readonly IMapper _mapper;
        public AuthorRepository( LibraryDBContext context, IMapper mapper ) {
            _dbset = context.Authors;
            _mapper = mapper;
        }

        public async Task AddAuthorAsync( Author author ) {
            var authorEntity = _mapper.Map<AuthorEntity>( author );
            await _dbset.AddAsync( authorEntity );
        }

        public async Task UpdateAuthorAsync( Author changedAuthor ) {
            var authorEntity = _mapper.Map<AuthorEntity>( changedAuthor );
            _dbset.Update( authorEntity );

        }

        public async Task DeleteAuthorAsync( Guid authorId ) {
            var authorToDelete = await _dbset
                .Include( a => a.Books )
                .AsNoTracking()
                .FirstOrDefaultAsync( a => a.Id == authorId )
                ?? throw new AuthorNotFoundException();

            _dbset.Remove( authorToDelete );
        }

        public async Task<IList<Author>> GetAuthorsAsync( int skip, int take ) {
            var authorEntities = await _dbset
                .AsNoTracking()
                .Skip( skip )
                .Take( take )
                .ToListAsync();
            return _mapper.Map<IList<Author>>( authorEntities );
        }

        public async Task<IList<Book>> GetBooksByAuthorAsync( Guid authorId, int skip, int take ) {
            var bookEntities = await _dbset
               .AsNoTracking()
               .Include( a => a.Books )
               .Where( a => a.Id == authorId )
               .SelectMany( x => x.Books )
               .Skip( skip )
               .Take( take )
               .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<Author> GetAuthorAsync( Guid authorId ) {
            var authorEntity = await _dbset
                .AsNoTracking()
                .Include( x => x.Books )
                .FirstOrDefaultAsync( a => a.Id == authorId )
                ?? throw new AuthorNotFoundException();
            return _mapper.Map<Author>( authorEntity );
        }

    }
}
