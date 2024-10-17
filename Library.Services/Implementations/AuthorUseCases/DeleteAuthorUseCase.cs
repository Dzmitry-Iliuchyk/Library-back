using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;

namespace Library.Application.Implementations.AuthorUseCases {
    public class DeleteAuthorUseCase: IDeleteAuthorUseCase {
        private readonly IUnitOfWork _unit;

        public DeleteAuthorUseCase( IUnitOfWork unit ) {
            _unit = unit;
        }

        public async Task Execute( Guid authorId ) {
            var authorToDelete = await _unit.authorRepository.GetAuthorWithBooksAsync( authorId );
            if (authorToDelete.Books != null && authorToDelete.Books.Any()) {
                return;
            }
            await _unit.authorRepository.DeleteAsync( authorToDelete );
            await _unit.Save();
        }
    }

}
