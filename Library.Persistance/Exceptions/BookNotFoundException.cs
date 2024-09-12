using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Exceptions {
    public class BookNotFoundException : DataAccessException{
        public override string Message => "Такой книги не найдено!";
    }
}
