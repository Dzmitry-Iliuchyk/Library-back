using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.BookUseCases {

    public interface IGiveBookToClientUseCase {
        Task Execute( Guid bookId, Guid clientId, int hoursToUse );

    }
}
