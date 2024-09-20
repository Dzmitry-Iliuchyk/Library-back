using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository {
    public class BookRepository: IBookRepository {
        private readonly DbSet<BookEntity> _dbSet;
        private readonly IMapper _mapper;
        public BookRepository( LibraryDBContext dbContext, IMapper mapper ) {
            this._dbSet = dbContext.Books;
            this._mapper = mapper;
        }

        public async Task CreateBookAsync( Book book ) {
            var bookEntity = _mapper.Map<BookEntity>( book );
            await _dbSet.AddAsync( bookEntity );

        }

        public async Task DeleteBookAsync( Guid bookId ) {
            var bookToDelete = await _dbSet
                .FirstOrDefaultAsync( x => x.Id == bookId )
                ?? throw new BookNotFoundException();
            _dbSet.Remove( bookToDelete );

        }

        public async Task<IList<Book>> GetBooksAsync(int skip, int take) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }
        public async Task<IList<Book>> GetFilteredBooksAsync(int skip, int take, string authorFilter, string titleFilter) {
            var bookEntities = await _dbSet
                .AsNoTracking()
                .Include(b=>b.Author)
                 .Where( b => ( string.IsNullOrEmpty( titleFilter )
                 || b.Title.Contains( titleFilter ))
                 && ( string.IsNullOrEmpty( titleFilter )
                 || ( b.Author.FirstName.Contains( authorFilter )
                 || b.Author.LastName.Contains( authorFilter ))) ) 
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<Book> GetBookAsync( Guid bookId ) {
            var bookEntity = await _dbSet
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.Id == bookId )
                ?? throw new BookNotFoundException();
            return _mapper.Map<Book>( bookEntity );
        }

        public async Task<Book> GetBookAsync( string ISBN ) {
            var bookEntity = await _dbSet
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.ISBN == ISBN )
                 ?? throw new BookNotFoundException();
            return _mapper.Map<Book>( bookEntity );
        }

        public Task UpdateBook( Book changedBook ) {
            var bookEntity = _mapper.Map<BookEntity>( changedBook );
            _dbSet.Update( bookEntity );
            return Task.CompletedTask;
        }

    }
}
