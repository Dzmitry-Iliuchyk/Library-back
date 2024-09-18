using Library.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.DataBase.Contexts;
using AutoMapper;
using Library.Application.Auth.Interfaces;

namespace Library.DataAccess.Repository {
    public class UnitOfWork: IUnitOfWork {
        private readonly LibraryDBContext _context ;
        
        private IDbContextTransaction? _objTran = null;

        public IUserRepository userRepository {  get; private set; }

        public IBookRepository bookRepository { get; private set; }
        public IAuthorRepository authorRepository { get; private set; }

        public IAuthRepository authRepository  { get; private set; }

    public UnitOfWork( LibraryDBContext context, IMapper mapper ) {
            _context = context;
            this.bookRepository = new BookRepository(context, mapper);
            this.authorRepository = new AuthorRepository( context, mapper );
            this.userRepository = new UserRepository( context, mapper );
            this.authRepository = new AuthRepository( context ,mapper );
        }

        public void CreateTransaction() {
            _objTran = _context.Database.BeginTransaction();
        }

        public void Commit() {

            _objTran?.Commit();
        }

        public void Rollback() {

            _objTran?.Rollback();
            
            _objTran?.Dispose();
        }
       
        public async Task Save() {
            try {
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }
            catch (DbUpdateException ex) {

                throw new Exception( ex.Message, ex );
            }
        }
        public void Dispose() {
            _context.Dispose();
        }
    }
}
