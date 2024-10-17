using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces.UserUseCases.DTO {
    public record UserDto(Guid Id, string UserName, string Email, string Password);
}
