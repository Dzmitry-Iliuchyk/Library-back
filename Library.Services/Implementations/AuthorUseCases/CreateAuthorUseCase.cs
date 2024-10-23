using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.AuthorUseCases;
using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;
using Library.Application.Exceptions;

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
            if (await _unit.authorRepository.Exist(author.Id)) {
                throw new AlreadyExistsException("Такой автор уже существует" );
            }
            await _unit.authorRepository.CreateAsync( author );
            await _unit.Save();
        }
    }

}
