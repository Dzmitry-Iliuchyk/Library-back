using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    internal class BookAlreadyFreeException: DomainException {
        public override string Message => $"Книга и так свободна";

    }
}
