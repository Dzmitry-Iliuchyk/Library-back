using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Auth.Interfaces {
    public interface IJwtProvider {
        string GenerateToken( User user );
    }
}
