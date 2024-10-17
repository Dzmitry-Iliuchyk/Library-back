using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    public class GiveBookToClientUseCase: IGiveBookToClientUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Book> _validator;

        public GiveBookToClientUseCase( IUnitOfWork unit, IValidator<Book> validator ) {
            _unit = unit;
            _validator = validator;
        }

        public async Task Execute( Guid bookId, Guid clientId, int hoursToUse ) {
            var book = await _unit.bookRepository.GetAsync( bookId );
            var client = await _unit.userRepository.GetAsync( clientId );
            if (book is FreeBook freeBook && client != null) {
                var takenBook = new TakenBook( freeBook, clientId, DateTime.UtcNow, DateTime.UtcNow.Add( TimeSpan.FromHours( hoursToUse ) ) );
                _validator.ValidateAndThrow( takenBook );
                await _unit.bookRepository.UpdateAsync( takenBook );
                await _unit.Save();
            }
        }
    }
}
/*

        public async Task GiveBookToClientAsync( Guid bookId, Guid clientId, int hoursToUse ) {

           

        }

        public async Task<IList<Book>> GetBooksAsync( int skip, int take ) {
            return await _unit.bookRepository.GetBooksAsync( skip, take );
        }
        public async Task<(IList<Book>, int)> GetFilteredBooksAsync( int skip, int take, string? authorFilter, string? titleFilter ) {
            return await _unit.bookRepository.GetFilteredBooksAsync( skip, take, authorFilter, titleFilter );
        }

        public async Task<Book> GetBookWithAuthorAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookWithAuthorAsync( bookId );
        }
        public async Task<Book> GetBookWithAllAsync( Guid bookId ) {
            return await _unit.bookRepository.GetBookWithAllAsync( bookId );
        }

        public async Task<Book> GetBookAsync( string ISBN ) {
            return await _unit.bookRepository.GetBookWithAuthorAsync( ISBN );
        }*/