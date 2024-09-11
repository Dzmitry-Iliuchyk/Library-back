using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    public class BookFreeException : Exception{

        public override string Message => $"Книга у другого клиента";

    }
}
