using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.AuthorUseCases;
using Library.Application.Exceptions;

namespace Library.Application.Implementations.AuthorUseCases {
    public class DeleteAuthorUseCase: IDeleteAuthorUseCase {
        private readonly IUnitOfWork _unit;

        public DeleteAuthorUseCase( IUnitOfWork unit ) {
            _unit = unit;
        }

        public async Task Execute( Guid authorId ) {
            var authorToDelete = await _unit.authorRepository.GetAuthorWithBooksAsync( authorId );
            if (authorToDelete != null) {
                throw new NotFoundException("Нет такого автора" );
            }
            if (authorToDelete.Books != null && authorToDelete.Books.Any()) {
                throw new CannotDeleteAuthorWithBooksException( $"Невозможно удалить автора {authorToDelete.FirstName} {authorToDelete.LastName} пока у него есть книги"  );
            }
            await _unit.authorRepository.DeleteAsync( authorToDelete );
            await _unit.Save();
        }
    }

}
