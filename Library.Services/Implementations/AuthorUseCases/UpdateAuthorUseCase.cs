using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;
using Library.Domain.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;

namespace Library.Application.Implementations.AuthorUseCases {
    public class UpdateAuthorUseCase: IUpdateAuthorUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Author> _validator;

        public UpdateAuthorUseCase( IUnitOfWork unit, IValidator<Author> validator ) {
            _unit = unit;
            _validator = validator;
        }

        public async Task Execute( AuthorDto authorDto ) {
            var authorInDb = await _unit.authorRepository.GetAuthorWithBooksAsync( authorDto.Id );
            var updatedAuthor = new Author( authorInDb.Id, authorDto.FirstName, authorDto.LastName, authorDto.Birthday, authorDto.Country );
            _validator.ValidateAndThrow( updatedAuthor );
            await _unit.authorRepository.UpdateAsync( updatedAuthor );
            await _unit.Save();
        }
    }

}
