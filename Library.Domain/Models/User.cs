using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models {
    public class User {
        public User( int id, string userName, string passwordHash, string email, IEnumerable<Book.Book> books = null) {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            Books = books;
        }

        public int Id { get; private set; }

        public string UserName { get; private set; }

        public string PasswordHash { get; private set; }

        public string Email { get; private set; }

        public IEnumerable<Book.Book> Books { get; private set; }
    }
}
