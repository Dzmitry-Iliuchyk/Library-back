using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions {
    public class BookTakenException :DomainException {
        public Guid ClientId { get; }

        public override string Message => $"Книга уже взята клиентом - {ClientId}";

        public BookTakenException( Guid clientId ) {
            ClientId = clientId;
        }
    }
}
