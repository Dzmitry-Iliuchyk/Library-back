using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DataBase.Entities {
    public class AuthorEntity {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<BookEntity>? Books { get; set; }
    }
}
