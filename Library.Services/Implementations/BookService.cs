using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations {
    public class BookService: IBookService {
        private readonly IBookRepository _bookRepository;
        private readonly IImageService _imageService;
        private readonly IValidator<Book> _validator;
        public BookService( IBookRepository bookRepository, IValidator<Book> validator, IImageService imageService  ) {
            _bookRepository = bookRepository;
            _validator = validator;
            _imageService = imageService;
        }
        public async Task CreateBookAsync( string ISBN, string title, string genre, string description, Guid authorId ) {
            var book = new FreeBook( Guid.NewGuid(), ISBN, title, genre, description, authorId );
            _validator.ValidateAndThrow( book );
            await _bookRepository.AddNewBook( book );
        }

        public async Task UpdateBookAsync( Guid bookId, string ISBN, string title, string genre, string description, Guid authorId ) {
            Book updatedBook;
            var bookInDb = await _bookRepository.GetBook( bookId );
            if (bookInDb is TakenBook taken) {
                updatedBook = new TakenBook( bookId, taken.ClientId, ISBN, title, genre, description, authorId, taken.TakenAt, taken.ReturnTo );
            } else
                updatedBook = new FreeBook( bookId, ISBN, title, genre, description, authorId );
            _validator.ValidateAndThrow( updatedBook );
            await _bookRepository.UpdateBook( updatedBook );
        }

        public async Task DeleteBookAsync( Guid bookId ) {
            
            await _bookRepository.DeleteBook( bookId );
            _imageService.DeleteImage(bookId);
        }

        public async Task FreeBookAsync( Guid bookId, Guid clientId ) {
            var book = await _bookRepository.GetBook( bookId );
            var freeBook = book.Free( clientId );
            await _bookRepository.UpdateBook( freeBook );
        }
        public async Task GiveBookToClientAsync( Guid bookId, Guid clientId, int hoursToUse ) {
            var book = await _bookRepository.GetBook( bookId );
            var freeBook = book.Take( clientId, TimeSpan.FromHours( hoursToUse ) );
            _validator.ValidateAndThrow( freeBook );
            await _bookRepository.UpdateBook( freeBook );
        }

        public async Task<IList<Book>> GetAllBooksAsync(int skip, int take) {
            return await _bookRepository.GetAllBooksAsync(skip, take);
        }

        public async Task<Book> GetBookAsync( Guid bookId ) {
            return await _bookRepository.GetBook( bookId );
        }

        public async Task<Book> GetBookAsync( string ISBN ) {
            return await _bookRepository.GetBook( ISBN );
        }
    }
}
