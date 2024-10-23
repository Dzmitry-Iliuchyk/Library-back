using FluentValidation;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.AuthorUseCases;
using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Domain.Models;
using Library.Application.Exceptions;
using AutoMapper;

namespace Library.Application.Implementations.AuthorUseCases {
    public class UpdateAuthorUseCase: IUpdateAuthorUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IValidator<Author> _validator;

        public UpdateAuthorUseCase( IUnitOfWork unit, IValidator<Author> validator, IMapper mapper) {
            _unit = unit;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task Execute( AuthorDto authorDto ) {
            var authorInDb = await _unit.authorRepository.GetAuthorWithBooksAsync( authorDto.Id );
            if (authorInDb == null) {
                throw new NotFoundException("Нет такого автора");
            }
            var updatedAuthor = _mapper.Map<Author>( authorDto );
            _validator.ValidateAndThrow( updatedAuthor );
            await _unit.authorRepository.UpdateAsync( updatedAuthor );
            await _unit.Save();
        }
    }

}
