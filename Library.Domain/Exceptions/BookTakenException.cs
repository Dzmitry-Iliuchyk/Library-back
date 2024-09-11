using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    public class BookTakenException :DomainException {
        public int ClientId { get; }

        public override string Message => $"Книга уже взята клиентом - {ClientId}";

        public BookTakenException( int clientId ) {
            ClientId = clientId;
        }
    }
}
