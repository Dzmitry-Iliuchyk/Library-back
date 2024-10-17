using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.AuthorUseCases;
using Library.Domain.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;

namespace Library.Application.Implementations.AuthorUseCases {
    public class CreateAuthorUseCase: ICreateAuthorUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IValidator<Author> _validator;

        public CreateAuthorUseCase( IUnitOfWork unit, IValidator<Author> validator ) {
            _unit = unit;
            _validator = validator;
        }

        public async Task Execute( CreateAuthorDTO createAuthorDTO ) {
            var author = new Author( Guid.NewGuid(), createAuthorDTO.FirstName, createAuthorDTO.LastName, createAuthorDTO.Birthday, createAuthorDTO.Country );
            _validator.ValidateAndThrow( author );
            await _unit.authorRepository.CreateAsync( author );
            await _unit.Save();
        }
    }

}
