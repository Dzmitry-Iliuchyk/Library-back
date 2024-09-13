using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations {
    public class AuthorService: IAuthorService {
        private readonly IAuthorRepository _repository;
        private readonly IValidator<Author> _validator;

        public AuthorService( IAuthorRepository bookRepository, IValidator<Author> validator ) {
            _repository = bookRepository;
            _validator = validator;
        }

        public async Task CreateAuthor( string firstName, string lastName, DateTime birthday, string country ) {
            var author = new Author(Guid.NewGuid(), firstName, lastName , birthday, country);
            var result = _validator.Validate( author );
            if (result.IsValid) {
                await _repository.AddAuthor( author );
            }
        }

        public async Task UpdateAuthor(Guid id, string firstName, string lastName, DateTime birthday, string country ) {
            var authorInDb = await _repository.GetAuthor( id );
            var updatedAuthor = new Author(authorInDb.Id, firstName, lastName, birthday, country);
            var result = _validator.Validate( updatedAuthor );
            if (result.IsValid) {
                await _repository.UpdateAuthor( updatedAuthor );
            }
        }

        public async Task DeleteAuthor( Guid authorId ) {

            await _repository.DeleteAuthor( authorId );

        }

        public async Task<IList<Author>> GetAllAuthors() {
            return await _repository.GetAllAuthors();
        }

        public async Task<IList<Book>> GetAllBooks( Guid authorId ) {
            return await _repository.GetAllBooksByAuthor( authorId );
        }

        public async Task<Author> GetAuthor( Guid authorId ) {
            return await _repository.GetAuthor( authorId );
        }
    }
}
