using Library.Domain.Interfaces;
using Library.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Implementations {
    public class BookService: IBookService {
        public Task AddNewBook( Book book ) {
            throw new NotImplementedException();
        }

        public Task AttachImageToBook( Book book, FileStream imageStream ) {
            throw new NotImplementedException();
        }

        public Task ChangeBook( int bookId, Book changedBook ) {
            throw new NotImplementedException();
        }

        public Task DeleteBook( int bookId ) {
            throw new NotImplementedException();
        }

        public Task FreeBook( Book book, int clientId ) {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> GetAllBooksAsync() {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook( int bookId ) {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook( string ISBN ) {
            throw new NotImplementedException();
        }

        public Task GiveBookToClient( Book book, int clientId ) {
            throw new NotImplementedException();
        }
    }
}
