using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    public class BookFreeException : DomainException{

        public override string Message => $"Книга у другого клиента";

    }
}
