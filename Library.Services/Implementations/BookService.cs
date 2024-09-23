using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models.Book;
using System.Net;

namespace Library.Application.Implementations {
    public class BookService: IBookService {
        private readonly IUnitOfWork _unit;
        private readonly IImageService _imageService;
        private readonly IValidator<Book> _validator;
        public BookService( IUnitOfWork unit, IValidator<Book> validator, IImageService imageService ) {
            _unit = unit;
            _validator = validator;
            _imageService = imageService;
        }
        public async Task<Guid> CreateBookAsync( string ISBN, string title, string genre, string description, Guid authorId ) {
            var book = new FreeBook(
                        id: Guid.NewGuid(),
                        ISBN: ISBN,
                        title: title,
                        genre: genre,
                        description: description,
                        authorId: authorId );
            _validator.ValidateAndThrow( book );
            await _unit.bookRepository.CreateBookAsync( book );
            await _unit.Save();
            return book.Id;
        }

        public async Task UpdateBookAsync( Guid bookId, string ISBN, string title, string genre, string description, Guid authorId ) {
            try {
                _unit.CreateTransaction();
                Book updatedBook;
                var bookInDb = await _unit.bookRepository.GetBookAsync( bookId );
                if (bookInDb is TakenBook taken) {
                    updatedBook = new TakenBook( 
                        id: bookId,
                        client_id: taken.ClientId,
                        ISBN: ISBN,
                        title: title, 
                        genre: genre,
                        description: description,
                        authorId: authorId,
                        takenAt: taken.TakenAt,
                        returnTo: taken.ReturnTo );
                } else
                    updatedBook = new FreeBook( 
                        id: bookId,
                        ISBN: ISBN,
                        title: title,
                        genre: genre,
                        description: description,
                        authorId: authorId );
                _validator.ValidateAndThrow( updatedBook );
                await _unit.bookRepository.UpdateBook( updatedBook );
                await _unit.Save();
                _unit.Commit();
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }
        }

        public async Task DeleteBookAsync( Guid bookId ) {
            try {
                _unit.CreateTransaction();
                await _unit.bookRepository.DeleteBookAsync( bookId );
                _imageService.DeleteImage( bookId );
                await _unit.Save();
                _unit.Commit();
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }
        }

        public async Task FreeBookAsync( Guid bookId, Guid clientId ) {
            var book = await _unit.bookRepository.GetBookAsync( bookId );
            var freeBook = book.Free( clientId );
            await _unit.bookRepository.UpdateBook( freeBook );
            await _unit.Save();
        }
        public async Task GiveBookToClientAsync( Guid bookId, Guid clientId, int hoursToUse ) {
            try {
                _unit.CreateTransaction();
                var book = await _unit.bookRepository.GetBookAsync( bookId );
                var takenBook = book.Take( clientId, TimeSpan.FromHours( hoursToUse ) );
                _validator.ValidateAndThrow( takenBook );
                await _unit.bookRepository.UpdateBook( takenBook );
                await _unit.Save();
                _unit.Commit();
            }
            catch (Exception) {
                _unit.Rollback();
                throw;
            }

        }

        public async Task<IList<Book>> GetBooksAsync( int skip, int take ) {
            return await _unit.bookRepository.GetBooksAsync( skip, take );
        }
        public async Task<(IList<Book>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter ) {
            return await _unit.bookRepository.GetFilteredBooksAsync(  skip,  take,  authorFilter,  titleFilter );
        }

        public async Task<Book> GetBookAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookAsync( bookId );
        }
        public async Task<Book> GetBookWithAllAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookWithAllAsync( bookId );
        }

        public async Task<Book> GetBookAsync( string ISBN ) {
            return await _unit.bookRepository.GetBookAsync( ISBN );
        }
    }
}
