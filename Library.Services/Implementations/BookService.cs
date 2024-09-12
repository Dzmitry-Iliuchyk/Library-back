using FluentValidation;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Implementations {
    public class BookService: IBookService {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<Book> _validator;
        public BookService( IBookRepository bookRepository, IValidator<Book> validator) {
            _bookRepository = bookRepository;
            _validator = validator;
        }
        public async Task AddNewBook( Book book ) {
            var result = _validator.Validate( book );
            if (result.IsValid) {
                await _bookRepository.AddNewBook( book );
            }
        }

        public Task AttachImageToBook( Book book, FileStream imageStream ) {
            throw new NotImplementedException();
        }

        public Task ChangeBook( Book changedBook ) {
            throw new NotImplementedException();
        }

        public Task DeleteBook( Guid bookId ) {
            throw new NotImplementedException();
        }

        public Task FreeBook( Book book, Guid clientId ) {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetAllBooksAsync() {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook( Guid bookId ) {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook( string ISBN ) {
            throw new NotImplementedException();
        }

        public Task GiveBookToClient( Book book, Guid clientId ) {
            throw new NotImplementedException();
        }
    }
}
