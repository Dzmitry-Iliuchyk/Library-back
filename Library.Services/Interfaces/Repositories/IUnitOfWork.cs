using Library.Application.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository userRepository { get; }

        public IBookRepository bookRepository { get; }
        public IAuthorRepository authorRepository { get; }
        public IAuthRepository authRepository { get; }

        void CreateTransaction();

        void Commit();

        void Rollback();

        Task Save();
    }
}
