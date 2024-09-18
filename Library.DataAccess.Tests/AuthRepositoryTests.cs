using AutoMapper;
using Library.Application.Auth.Enums;
using Library.DataAccess.DataBase.Configuration;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Repository;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json.Linq;

namespace Library.DataAccess.Tests {
    [TestFixture]
    public class AuthRepositoryTests {
        private LibraryDBContext _context;
        private IMapper _mapper;
        private Mock<IOptions<AuthorizationOptions>> _mockOptions;
        private static IPasswordHasher<User> hasher = new PasswordHasher<User>();
        private AuthRepository _repository;
        private (Guid, Guid) guidsUser;
        private (Guid, Guid) guidsRefreshToken;
        private List<UserEntity> _users;
        private string refreshToken = "bqjLt%2FKwvW9AFRaFZoKmXY%2BvqedYc%2FWZIHYv7AJumoc%3D";
        [ SetUp]
        public void Setup() {
            guidsUser= new( Guid.NewGuid(), Guid.NewGuid() );
            guidsRefreshToken= new (Guid.NewGuid(), Guid.NewGuid() );
            _users = new List<UserEntity>() {
            new UserEntity{ Id = guidsUser.Item1,UserName = "userNameasda",Email = "validdadad@mail.cim", PasswordHash = hasher.HashPassword(null, "password"), Books = new List<BookEntity>()
            {
            new BookEntity{
                Id= Guid.NewGuid(),
                ISBN= "1234567890",
                Title= "Test Title",
                Genre= "Test Genre",
                Description= "Test DescriptionTest DescriptionTest DescriptionTest Description",
                Author = new AuthorEntity {
                    Id = Guid.NewGuid(),
                    FirstName = "Teodor",
                    LastName = "Potter",
                    Birthday = new DateTime( 1999, 1, 1 ),
                    Country = "Belarus"
                },
                ClientId= guidsUser.Item1,
                TakenAt = DateTime.UtcNow.AddDays(-3),
                ReturnTo = DateTime.UtcNow.AddDays(1),
                BookType = DataBase.Enums.BookType.Taken
            },
            new BookEntity{
                Id= Guid.NewGuid(),
                ISBN= "1236566890",
                Title= "Test Title2",
                Genre= "Test Genre2",
                Description= "Test Descript2ionTest Descr2iptionTest Description2Test Description",
                Author =  new AuthorEntity {
                    Id = Guid.NewGuid(),
                    FirstName = "Albus",
                    LastName = "Dambldor",
                    Birthday = new DateTime( 1929, 1, 1 ),
                    Country = "England"
                },
                ClientId= guidsUser.Item1,
                TakenAt = DateTime.UtcNow.AddDays(-3),
                ReturnTo = DateTime.UtcNow.AddDays(1),
                BookType = DataBase.Enums.BookType.Taken
            }
            }
            },
            new UserEntity{ Id = guidsUser.Item2, UserName = "userName2dada", Email = "valid2adasd@mail.cim", PasswordHash = hasher.HashPassword(null, "password")}
        };
            _mapper = Helpers.ConfigureMapper();
            _mockOptions = new Mock<IOptions<AuthorizationOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( Helpers.AuthOptions() );
            _context = new LibraryDBContext( new DbContextOptionsBuilder<LibraryDBContext>().UseInMemoryDatabase("Test").Options, _mockOptions.Object );
            _repository = new AuthRepository( _context, _mapper );

        }

        [Test]
        public async Task RemoveRefreshToken_ShouldRemoveToken() {
            _context.Users.AddRange( _users );
            _context.RefreshTokens.AddRange( new RefreshToken() { Id = guidsRefreshToken.Item1, ExpiryDate = DateTime.UtcNow.AddDays( -2 ), Token = "2BvqedYc%2FWZIHYv7AJumoc%3D", UserId = guidsUser.Item1 },
                new RefreshToken() { Id = guidsRefreshToken.Item2, ExpiryDate = DateTime.UtcNow.AddDays( 2 ), Token = refreshToken, UserId = guidsUser.Item2 } );
            _context.SaveChanges();
            //Act
            await _repository.RemoveRefreshToken( refreshToken );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.RefreshTokens.Count(), Is.EqualTo( 1 ) );
        }
        [Test]
        public async Task RemoveAllRefreshTokens_ShouldRemoveToken() {

            _context.Users.AddRange( _users );
            _context.RefreshTokens.AddRange( new RefreshToken() { Id = guidsRefreshToken.Item1, ExpiryDate = DateTime.UtcNow.AddDays( -2 ), Token = "2BvqedYc%2FWZIHYv7AJumoc%3D", UserId = guidsUser.Item1 },
                new RefreshToken() { Id = guidsRefreshToken.Item2, ExpiryDate = DateTime.UtcNow.AddDays( 2 ), Token = refreshToken, UserId = guidsUser.Item2 } );
            _context.SaveChanges();
            //Act
            await _repository.RemoveAllRefreshTokens( guidsUser.Item1 );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.RefreshTokens.Where(x=>x.UserId== guidsUser.Item1 ).Count(), Is.EqualTo( 0 ) );
        }
        [Test]
        public async Task GetActiveRefreshToken_ShouldReturnActiveTokenOrNull() {
            _context.Users.AddRange( _users );
            _context.RefreshTokens.AddRange( new RefreshToken() { Id = guidsRefreshToken.Item1, ExpiryDate = DateTime.UtcNow.AddDays( -2 ), Token = "2BvqedYc%2FWZIHYv7AJumoc%3D", UserId = guidsUser.Item1 },
                new RefreshToken() { Id = guidsRefreshToken.Item2, ExpiryDate = DateTime.UtcNow.AddDays( 2 ), Token = refreshToken, UserId = guidsUser.Item2 } );
            _context.SaveChanges();
            //Act
            var user1Token = await _repository.GetActiveRefreshToken( guidsUser.Item1 );
            var user2Token = await _repository.GetActiveRefreshToken( guidsUser.Item2 );

            //Assert
            Assert.That( user1Token, Is.EqualTo( null ) );
            Assert.That( user2Token, Is.EqualTo( refreshToken ) );
        }
        [Test]
        public async Task SaveRefreshToken_ShouldSaveToken() {
            _context.Users.AddRange( _users );
            _context.RefreshTokens.AddRange( new RefreshToken() { Id = guidsRefreshToken.Item1, ExpiryDate = DateTime.UtcNow.AddDays( -2 ), Token = "2BvqedYc%2FWZIHYv7AJumoc%3D", UserId = guidsUser.Item1 },
                new RefreshToken() { Id = guidsRefreshToken.Item2, ExpiryDate = DateTime.UtcNow.AddDays( 2 ), Token = refreshToken, UserId = guidsUser.Item2 } );
            _context.SaveChanges();
            var Obj = new RefreshToken {
                Id = Guid.NewGuid(),
                UserId = guidsUser.Item1,
                Token = "SuperSecretToken",
                ExpiryDate = DateTime.UtcNow.AddDays( 7 )
            };
            //Act
           
            await _repository.SaveRefreshToken(Obj.UserId, Obj.Token );
            await _context.SaveChangesAsync();
            //Assert
            var tokenInDb = await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token == Obj.Token);
            Assert.That( tokenInDb.Token, Is.EqualTo( Obj.Token ) );
            Assert.That( _context.RefreshTokens.Count(), Is.EqualTo( 3 ) );
        }
        [Test]
        public async Task AddUserToGroup_ShouldAddUserToGroup() {
            _context.Database.EnsureCreated();
            _context.Users.AddRange( _users );
            _context.SaveChanges();
            var user = _users.First( x => x.Id == guidsUser.Item1 );
            //Act

            await _repository.AddUserToGroup( user.Id  , AccessGroupEnum.User );
            await _context.SaveChangesAsync();
            //Assert
            var userInDb = await _context
                .Users
                .AsNoTracking()
                .Include(x=>x.Groups)
                .FirstOrDefaultAsync(x=>x.Id== guidsUser.Item1 );
            Assert.That( userInDb.Groups.Count(), Is.EqualTo( 1 ) );
            Assert.That( userInDb.Groups.Any(x=>x.Name == AccessGroupEnum.User.ToString()));
        }
        [Test]
        public async Task RemoveUserFromGroup_ShouldRemoveUserFromGroup() {
            //arrange
            _context.Database.EnsureCreated();
            _context.Users.AddRange( _users );
            _context.SaveChanges();
            var user = _users.First( x => x.Id == guidsUser.Item1 );
            await _repository.AddUserToGroup( user.Id, AccessGroupEnum.User );
            await _context.SaveChangesAsync();
           
            //Act
            await _repository.RemoveUserFromGroup( user.Id , AccessGroupEnum.User );
            await _context.SaveChangesAsync();
            //Assert
            var userInDb = await _context
                .Users
                .AsNoTracking()
                .Include(x=>x.Groups)
                .FirstOrDefaultAsync(x=>x.Id== guidsUser.Item1 );
            Assert.That( userInDb.Groups.Count(), Is.EqualTo( 0 ) );
            Assert.That( !userInDb.Groups.Any(x=>x.Name == AccessGroupEnum.User.ToString()));
        }
        
        [Test]
        public async Task GetUserGroups_ShouldReturnGroups() {
            //arrange
            _context.Database.EnsureCreated();
            _context.Users.AddRange( _users );
            await _context.SaveChangesAsync();
            await _repository.AddUserToGroup( guidsUser.Item1, AccessGroupEnum.User );
            await _context.SaveChangesAsync();
         
            //Act
            var result = await _repository.GetUserGroups( guidsUser.Item1 );

            //Assert

            Assert.That( result.Count(), Is.EqualTo( 1 ) );
            Assert.That( result.Contains( AccessGroupEnum.User ) );
        }
        [Test]
        public async Task GetUserPermissions_ShouldReturnPermissions() {
            //arrange
            _context.Database.EnsureCreated();
            _context.Users.AddRange( _users );
            await _context.SaveChangesAsync();
            await _repository.AddUserToGroup( guidsUser.Item1, AccessGroupEnum.User );
            await _context.SaveChangesAsync();
         
            //Act
            var result = await _repository.GetUserPermissions( guidsUser.Item1 );

            //Assert

            Assert.That( result.Count(), Is.EqualTo( 1 ) );
            Assert.That( result.Contains( PermissionEnum.Read ) );
        }

        [TearDown]
        public void TearDown() {
            _context.Database.EnsureDeleted();
            _context.ChangeTracker.Clear();
            _context.Dispose();
        }
    }
}