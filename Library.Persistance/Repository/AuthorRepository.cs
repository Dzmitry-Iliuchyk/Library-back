﻿using AutoMapper;
using Library.Application.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository {
    public class AuthorRepository: IAuthorRepository {
        private readonly LibraryDBContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorRepository( LibraryDBContext context, IMapper mapper ) {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddAuthor( Author author ) {
            var authorEntity = _mapper.Map<AuthorEntity>( author );
            await _dbContext.Authors.AddAsync( authorEntity );
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAuthor( Author changedAuthor ) {
            var authorEntity = _mapper.Map<AuthorEntity>( changedAuthor );
            _dbContext.Authors.Update( authorEntity );
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthor( Guid authorId ) {
            var authorToDelete = await _dbContext
                .Authors
                .FirstOrDefaultAsync( a => a.Id == authorId )
                ?? throw new AuthorNotFoundException();
            _dbContext.Authors.Remove( authorToDelete );
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Author>> GetAllAuthors() {
            var authorEntities = await _dbContext
                .Authors
                .AsNoTracking()
                //.Include( a => a.Books )
                .ToListAsync();
            return _mapper.Map<IList<Author>>( authorEntities );
        }

        public async Task<IList<Book>> GetAllBooksByAuthor( Guid authorId ) {
            var bookEntities = await _dbContext
               .Authors
               .AsNoTracking()
               .Include( a => a.Books )
               .Where(a=>a.Id == authorId)
               .SelectMany( x => x.Books )
               .ToListAsync();

            return _mapper.Map<IList<Book>>( bookEntities );
        }

        public async Task<Author> GetAuthor( Guid authorId ) {
            var authorEntity = await _dbContext
                .Authors
                .AsNoTracking()
                .Include( x => x.Books )
                .FirstOrDefaultAsync( a => a.Id == authorId )
                ?? throw new AuthorNotFoundException();
            return _mapper.Map<Author>( authorEntity );
        }
    }
}
