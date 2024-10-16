using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DataBase.Entities {
    public class AuthorEntity : DbEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }

        public IEnumerable<BookEntity>? Books { get; set; }
    }
}
