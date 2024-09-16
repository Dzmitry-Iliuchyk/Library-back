using Library.Application.Auth.Interfaces;
using Library.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Infrastracture {
    public enum CustumClaimTypes {
        UserId,
        Email
    }
    public class JwtProvider: IJwtProvider {
        private readonly JwtOptions _options;

        public JwtProvider( IOptions<JwtOptions> options ) {
            this._options = options.Value;
        }

        public string GenerateToken( User user ) {
            var claims = new List<Claim>() {
                new Claim(CustumClaimTypes.UserId.ToString(), user.Id.ToString()),
                new Claim(CustumClaimTypes.Email.ToString(), user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes( _options.ExpiresMinutes ),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _options.Secret ) ),
                    algorithm: SecurityAlgorithms.HmacSha256
                    )
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken( token );
            return tokenValue;
        }
    }
}
