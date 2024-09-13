using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations {
    public class BookService: IBookService {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<Book> _validator;
        public BookService( IBookRepository bookRepository, IValidator<Book> validator ) {
            _bookRepository = bookRepository;
            _validator = validator;
        }
        public async Task AddNewBook( string ISBN, string title, string genre, string description, Guid authorId ) {
            var book = new FreeBook( Guid.NewGuid(), ISBN, title, genre, description, authorId );
            var result = _validator.Validate( book );
            if (result.IsValid) {
                await _bookRepository.AddNewBook( book );
            }
        }

        public async Task UpdateBook( Guid bookId, string ISBN, string title, string genre, string description, Guid authorId ) {
            Book updatedBook;
            var bookInDb = await _bookRepository.GetBook( bookId );
            if (bookInDb is TakenBook taken) {
                updatedBook = new TakenBook( bookId, taken.ClientId, ISBN, title, genre, description, authorId, taken.TakenAt, taken.ReturnTo );
            } else
                updatedBook = new FreeBook( bookId, ISBN, title, genre, description, authorId );

            var result = _validator.Validate( updatedBook );
            if (result.IsValid) {
                await _bookRepository.UpdateBook( updatedBook );
            }
        }

        public async Task DeleteBook( Guid bookId ) {
            await _bookRepository.DeleteBook( bookId );
        }

        public async Task FreeBook( Guid bookId, Guid clientId ) {
            var book = await _bookRepository.GetBook( bookId );
            var freeBook = book.Free( clientId );
            await _bookRepository.UpdateBook( freeBook );
        }
        public async Task GiveBookToClient( Guid bookId, Guid clientId, TimeSpan periodToUse ) {
            var book = await _bookRepository.GetBook( bookId );
            if (periodToUse > TimeSpan.Zero) {
                var freeBook = book.Take( clientId, periodToUse );
                await _bookRepository.UpdateBook( freeBook );
            }
        }

        public async Task<IList<Book>> GetAllBooksAsync() {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBook( Guid bookId ) {
            return await _bookRepository.GetBook( bookId );
        }

        public async Task<Book> GetBook( string ISBN ) {
            return await _bookRepository.GetBook( ISBN );
        }

        public Task GetImageToBook( Guid bookId ) {
            throw new NotImplementedException();
        }

        public Task AttachImageToBook( Guid bookId, FileStream imageStream ) {
            throw new NotImplementedException();
        }
    }
}
