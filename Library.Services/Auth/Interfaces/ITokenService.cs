using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Auth.Interfaces {
    public interface ITokenService {
        string GenerateRefreshToken();
        string GenerateToken( User user );
        ClaimsPrincipal GetPrincipalFromExpiredToken( string token );
    }
}
