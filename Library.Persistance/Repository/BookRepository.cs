using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository {
    public class BookRepository: IBookRepository {
        private readonly LibraryDBContext _dbContext;
        private readonly IMapper _mapper;
        public BookRepository( LibraryDBContext dbContext, IMapper mapper ) {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task AddNewBook( Book book ) {
            var bookEntity = _mapper.Map<BookEntity>( book );
            await _dbContext.Books.AddAsync( bookEntity );
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook( Guid bookId ) {
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync( x => x.Id == bookId )
                ?? throw new BookNotFoundException();
            _dbContext.Books.Remove( bookToDelete );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Book>> GetAllBooksAsync(int skip, int take) {
            var bookEntities = await _dbContext
                .Books
                .AsNoTracking()
                .Include( x => x.Author )
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<Book> GetBook( Guid bookId ) {
            var bookEntity = await _dbContext
                .Books
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.Id == bookId )
                ?? throw new BookNotFoundException();
            return _mapper.Map<Book>( bookEntity );
        }

        public async Task<Book> GetBook( string ISBN ) {
            var bookEntity = await _dbContext
                .Books
                .AsNoTracking()
                .Include( x => x.Author )
                .FirstOrDefaultAsync( b => b.ISBN == ISBN )
                 ?? throw new BookNotFoundException();
            return _mapper.Map<Book>( bookEntity );
        }

        public Task UpdateBook( Book changedBook ) {
            var bookEntity = _mapper.Map<BookEntity>( changedBook );
            _dbContext.Books.Update( bookEntity );
            return Task.CompletedTask;
        }

    }
}
