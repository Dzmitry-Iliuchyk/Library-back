using Library.Domain.Models;
using Library.Infrastracture;
using Library.Infrastracture.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Tests {
    [TestFixture]
    public class TokenServiceTests {
        private TokenService _tokenService;
        private Mock<IOptions<JwtOptions>> _mockOptions;
        private IPasswordHasher<User> _hasher;
        [SetUp]
        public void SetUp() {
            _mockOptions = new Mock<IOptions<JwtOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( new JwtOptions {
                Issuer = "testIssuer",
                Audience = "testAudience",
                Secret = "kafghbaihbgaibhgahbgfbhagfhbgafaljfhojfhahfiahfjasfasfaffhbkaghba",
                ExpiresMinutes = 30
            } );
            _hasher = new PasswordHasher<User>();
            _tokenService = new TokenService( _mockOptions.Object );
        }

        [Test]
        public void GenerateToken_ShouldReturnToken() {
            var password = "password";
            var userId = Guid.NewGuid();
            var passwordHash = _hasher.HashPassword( null, password );
            var user = new User( id: userId,
                userName: "testUser",
                passwordHash: passwordHash,
                email: "valid@mail.com" );

            var token = _tokenService.GenerateToken( user );

            Assert.IsNotNull( token );
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken( token );
            Assert.That( jwtToken.Issuer, Is.EqualTo( "testIssuer" ) );
            Assert.That( jwtToken.Audiences.First(), Is.EqualTo( "testAudience" ) );
            Assert.That( jwtToken.Claims.First( c => c.Type == CustumClaimTypes.UserId ).Value, Is.EqualTo( user.Id.ToString() ) );
            Assert.That( jwtToken.Claims.First( c => c.Type == CustumClaimTypes.Email ).Value, Is.EqualTo( user.Email ) );
        }

        [Test]
        public void GenerateRefreshToken_ShouldReturnBase64String() {
            var refreshToken = _tokenService.GenerateRefreshToken();

            Assert.IsNotNull( refreshToken );
            Assert.DoesNotThrow( () => Convert.FromBase64String( refreshToken ) );
        }

        [Test]
        public void GetUserIdFromExpiredToken_ShouldReturnUserId() {
            var password = "password";
            var userId = Guid.NewGuid();
            var passwordHash = _hasher.HashPassword( null, password );
            var user = new User( id: userId,
                userName: "testUser",
                passwordHash: passwordHash,
                email: "valid@mail.com" );
            var token = _tokenService.GenerateToken( user );

            var userIdFromToken = _tokenService.GetUserIdFromExpiredToken( token );

            Assert.That( userIdFromToken, Is.EqualTo( user.Id ) );    
        }
    }
}
