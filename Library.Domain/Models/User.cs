using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models {
    public class User: Entity {
        public User( Guid id, string userName, string passwordHash, string email, IEnumerable<Book.Book> books = null): base(id) {
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            Books = books;
        }

        public string UserName { get; private set; }

        public string PasswordHash { get; private set; }

        public string Email { get; private set; }

        public IEnumerable<Book.Book> Books { get; private set; }
    }
}
