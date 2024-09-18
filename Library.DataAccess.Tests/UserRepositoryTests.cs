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

namespace Library.DataAccess.Tests {
    [TestFixture]
    public class UserRepositoryTests {
        private LibraryDBContext _context;
        private IMapper _mapper;
        private Mock<IOptions<AuthorizationOptions>> _mockOptions;
        private static IPasswordHasher<User> hasher = new PasswordHasher<User>();
        private UserRepository _repository;
        private static (Guid, Guid) guidsUser = new( Guid.NewGuid(), Guid.NewGuid() );
        private List<UserEntity> _users = new List<UserEntity>() {
            new UserEntity{ Id = guidsUser.Item1,UserName = "userName",Email = "valid@mail.cim", PasswordHash = hasher.HashPassword(null, "password"), Groups = [ new AccessGroupEntity() { Id = 1 } ], Books = new List<BookEntity>()
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
            new UserEntity{ Id = guidsUser.Item2, UserName = "userName2", Email = "valid2@mail.cim", PasswordHash = hasher.HashPassword(null, "password"), Groups = [ new AccessGroupEntity() { Id = 2 } ] }
        };

        [SetUp]
        public void Setup() {

            _mapper = Helpers.ConfigureMapper();
            _mockOptions = new Mock<IOptions<AuthorizationOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( Helpers.AuthOptions() );
            _context = new LibraryDBContext( new DbContextOptionsBuilder<LibraryDBContext>().UseInMemoryDatabase( "TestDb" ).Options, _mockOptions.Object );
            _context.Database.EnsureDeleted();
            _context.ChangeTracker.Clear();
            _context.Users.AddRange( _users );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            _repository = new UserRepository( _context, _mapper );
        }
        [Test]
        public async Task CreateUserAsync_ShouldAddUser() {
            //Arrange
            var userId = Guid.NewGuid();
            var newUser = new User(
                id: userId,
                userName: "NewUserName",
                email: "Newvalid2@mail.cim",
                passwordHash: hasher.HashPassword( null, "password" ) );
            //Act
            await _repository.CreateUserAsync( newUser );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.Users.First( x => x.Id == newUser.Id ).Id, Is.EqualTo( newUser.Id ) );
        }

        [Test]
        public async Task UpdateUser_ShouldUpdateUser() {
            //Arrange
            var userId = Guid.NewGuid();
            var updatedUser = new User(
                id: guidsUser.Item2,
                userName: "UpdatedUserName",
                email: "Newvalid2@mail.cim",
                passwordHash: hasher.HashPassword( null, "password" ) );
            //Act
            await _repository.UpdateUser( updatedUser );
            await _context.SaveChangesAsync();
            var updatedAuthorInDb = _context.Users.First( x => x.Id == guidsUser.Item2 );
            //Assert
            Assert.That( updatedAuthorInDb.UserName, Is.EqualTo( updatedUser.UserName ) );
        }
        [Test]
        public async Task DeleteUserAsync_ShouldDeleteUser() {

            //Act
            await _repository.DeleteUserAsync( guidsUser.Item1 );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.Users.Count(), Is.EqualTo( 1 ) );
        }
        [Test]
        public async Task GetUsersAsync_ShouldReturnUsers() {

            //Act
            var result = await _repository.GetUsersAsync( 0, 2 );
            //Assert
            Assert.That( result.Count, Is.EqualTo( 2 ) );
        }
        [Test]
        public async Task GetUserAsync_Guid_ShouldReturnUser() {

            //Act
            var result = await _repository.GetAsync( guidsUser.Item1 );
            //Assert
            Assert.That( result.Id, Is.EqualTo( guidsUser.Item1 ) );
        }
        [Test]
        public async Task GetUserAsync_email_ShouldReturnUser() {

            //Act
            var result = await _repository.GetAsync( _users.First(x=>x.Id == guidsUser.Item1 ).Email );
            //Assert
            Assert.That( result.Id, Is.EqualTo( guidsUser.Item1 ) );
        }
        
        [Test]
        public async Task GetBooksAsync_ShouldReturnBooks() {

            //Act
            var result = await _repository.GetBooksAsync(guidsUser.Item1 ,0, 2 );
            //Assert
            Assert.That( result.Count, Is.EqualTo( 2 ) );
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }
    }
}