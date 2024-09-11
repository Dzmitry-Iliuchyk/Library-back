using AutoMapper;
using Library.DataAccess.DataBase.Contexts;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository {
    public class AuthorRepository: IAuthorRepository {
        private readonly LibraryDBContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorRepository(LibraryDBContext context, IMapper mapper ) {
            _dbContext = context;
            _mapper = mapper;
        }

        public Task AddAuthor( Author author) {
            throw new NotImplementedException();
        }

        public Task ChangeAuthor( int authorId, Author changedAuthor ) {
            throw new NotImplementedException();
        }

        public Task DeleteAuthor( int authorId ) {
            throw new NotImplementedException();
        }

        public Task<IList<Author>> GetAllAuthors() {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetAllBooks( int authorId ) {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthor( int authorId ) {
            throw new NotImplementedException();
        }
    }
}
