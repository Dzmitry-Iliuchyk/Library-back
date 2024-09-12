using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Exceptions {
    public class AuthorNotFoundException : DataAccessException{
        public override string Message => "Такого автора не существует!";
    }
}
