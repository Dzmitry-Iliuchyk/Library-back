using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Implementations {
    public class AuthorService: IAuthorService {
        private readonly IBookRepository _repository;

        public AuthorService(IBookRepository bookRepository) {
            _repository = bookRepository;
        }

        public Task AddAuthor( Author author ) {
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
