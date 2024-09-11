using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    internal class BookAlreadyFreeException: Exception {
        public override string Message => $"Книга уже свободна";

    }
}
