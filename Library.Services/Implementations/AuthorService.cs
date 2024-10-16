using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations
{
    public class AuthorService: IAuthorService {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Author> _validator;

        public AuthorService( IUnitOfWork unitOfWork, IValidator<Author> validator ) {
            _unit = unitOfWork;
            _validator = validator;
        }

        public async Task CreateAuthorAsync( string firstName, string lastName, DateTime birthday, string country ) {
            var author = new Author( id: Guid.NewGuid(),
                firstName: firstName,
                lastName: lastName,
                birthday: birthday,
                country: country );
            _validator.ValidateAndThrow( author );
            await _unit.authorRepository.CreateAsync( author );
            await _unit.Save();
        }

        public async Task UpdateAuthorAsync( Guid id, string firstName, string lastName, DateTime birthday, string country ) {
            var authorInDb = await _unit.authorRepository.GetAuthorWithBooksAsync( id );
            var updatedAuthor = new Author( id: authorInDb.Id,
                firstName: firstName,
                lastName: lastName,
                birthday: birthday,
                country: country );
            _validator.ValidateAndThrow( updatedAuthor );
            await _unit.authorRepository.UpdateAsync( updatedAuthor );
            await _unit.Save();
        }

        public async Task DeleteAuthorAsync( Guid authorId ) {

            var authorToDelete = await _unit.authorRepository.GetAuthorWithBooksAsync( authorId );
            if (authorToDelete.Books != null && authorToDelete.Books.Any()) {
                return;
            }
            await _unit.authorRepository.DeleteAsync( authorToDelete );
            await _unit.Save();

        }

        public async Task<IList<Author>> GetAuthorsAsync( int skip, int take ) {
            return await _unit.authorRepository.GetManyAsync( skip, take );
        }

        public async Task<IList<Book>> GetBooksAsync( Guid authorId, int skip, int take ) {
            return await _unit.authorRepository.GetBooksByAuthorAsync( authorId, skip, take );
        }

        public async Task<Author> GetAuthorAsync( Guid authorId ) {
            return await _unit.authorRepository.GetAuthorWithBooksAsync( authorId );
        }
    }
}
