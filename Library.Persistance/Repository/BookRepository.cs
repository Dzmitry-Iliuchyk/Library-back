using AutoMapper;
using Library.DataAccess.DataBase.Contexts;
using Library.Domain.Models.Book;
using Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository {
    public class BookRepository: IBookRepository {
        private readonly LibraryDBContext _dbContext;
        private readonly IMapper _mapper;
        IDbContextTransaction _dbTransaction;
        public BookRepository( LibraryDBContext dbContext, IMapper mapper ) {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public Task AddNewBook( Book book ) {
            throw new NotImplementedException();

        }

        public Task DeleteBook( int bookId ) {
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

        public Task UpdateBook( int bookId, Book changedBook ) {
            throw new NotImplementedException();
        }
    }
}
