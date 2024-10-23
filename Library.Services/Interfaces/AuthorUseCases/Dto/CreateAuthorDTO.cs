using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.AuthorUseCases.Dto {
    public record CreateAuthorDTO(string FirstName, string LastName, DateTime Birthday, string Country);

}
