using FluentValidation;
using Library.Application.Auth.Enums;
using Library.Application.Auth.Interfaces;
using Library.Application.Exceptions;
using Library.Application.Implementations;
using Library.Application.Interfaces.Repositories;
using Library.Application.Validator;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Library.Application.Tests
{
    public class UserServiceTests {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ITokenService> _mockTokenService;
        private IValidator<User> _validator;
        private IPasswordHasher<User> _hasher;
        private UserService _userService;
        [SetUp]
        public void SetUp() {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTokenService = new Mock<ITokenService>();
            _validator = new UserValidator();
            _hasher = new PasswordHasher<User>();
            _userService = new UserService( _mockUnitOfWork.Object, _validator, _hasher, _mockTokenService.Object );
        }

        [Test]
        public async Task Register_ShouldReturnTokens_WhenUserIsValid() {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password";
            var refreshToken = "refreshToken";
            var token = "token";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( Guid.NewGuid(), userName, email, hashedPassword );

            _mockUnitOfWork.Setup( u => u.userRepository.CreateAsync( user ) ).Returns( Task.CompletedTask );
            _mockUnitOfWork.Setup( u => u.authRepository.AddUserToGroup( user.Id, Auth.Enums.AccessGroupEnum.User ) ).Returns( Task.CompletedTask );
            _mockUnitOfWork.Setup( u => u.authRepository.SaveRefreshToken( user.Id, It.IsAny<string>() ) ).Returns( Task.CompletedTask );
            _mockTokenService.Setup( t => t.GenerateToken( It.IsAny<User>() ) ).Returns( token );
            _mockTokenService.Setup( t => t.GenerateRefreshToken() ).Returns( refreshToken );

            // Act
            var result = await _userService.Register( userName, email, password );

            // Assert
            Assert.That( result.Item1, Is.EqualTo( token ) );
            Assert.That( result.Item2, Is.EqualTo( refreshToken ) );
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Exactly( 3 ) );
            _mockUnitOfWork.Verify( u => u.userRepository.CreateAsync( It.IsAny<User>() ), Times.Exactly( 1 ) );
            _mockUnitOfWork.Verify( u => u.authRepository.AddUserToGroup( It.IsAny<Guid>(), It.IsAny<AccessGroupEnum>() ), Times.Once() );
            _mockUnitOfWork.Verify( u => u.authRepository.SaveRefreshToken( It.IsAny<Guid>(), It.IsAny<string>() ), Times.Once() );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }
        [Test]
        public void Register_ShouldThrowException_WhenUserIsInvalid() {
            // Arrange
            var userName = "ter";
            var email = "test.com";
            var password = "p";


            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( () => _userService.Register( userName, email, password ) );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
        }
        [Test]
        public async Task Login_ShouldReturnTokens_WhenCredentialsAreValid() {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( Guid.NewGuid(), "testUser", hashedPassword, email );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( email ) ).ReturnsAsync( user );
            _mockUnitOfWork.Setup( u => u.authRepository.RemoveAllRefreshTokens( user.Id ) ).Returns( Task.CompletedTask );
            _mockUnitOfWork.Setup( u => u.authRepository.SaveRefreshToken( user.Id, It.IsAny<string>() ) ).Returns( Task.CompletedTask );

            _mockTokenService.Setup( t => t.GenerateToken( It.IsAny<User>() ) ).Returns( "token" );
            _mockTokenService.Setup( t => t.GenerateRefreshToken() ).Returns( "refreshToken" );

            // Act
            var result = await _userService.Login( email, password );

            // Assert
            Assert.That( result.Item1, Is.EqualTo( "token" ) );
            Assert.That( result.Item2, Is.EqualTo( "refreshToken" ) );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Exactly( 2 ) );
            _mockUnitOfWork.Verify( u => u.authRepository.SaveRefreshToken( It.IsAny<Guid>(), It.IsAny<string>() ), Times.Once() );
            _mockUnitOfWork.Verify( u => u.authRepository.RemoveAllRefreshTokens( It.IsAny<Guid>() ), Times.Once() );
        }

        [Test]
        public void Login_ShouldThrowException_WhenPasswordIsInvalid() {
            // Arrange
            var email = "test@example.com";
            var wrongPassword = "wrongPassword";
            var password = "Password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( Guid.NewGuid(), "testUser", hashedPassword, email );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( email ) ).ReturnsAsync( user );

            // Act & Assert
            Assert.ThrowsAsync<InvalidPasswordException>( () => _userService.Login( email, wrongPassword ) );
        }
        [Test]
        public async Task LoginByRefresh_ShouldReturnNewToken_WhenRefreshTokenIsValid() {
            // Arrange
            var userId = Guid.NewGuid();
            var accessToken = "expiredAccessToken";
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( userId, "testUser", hashedPassword, email );
            var refreshToken = new Auth.Entities.RefreshToken() {
                Id = Guid.NewGuid(),
                Token = "validRefreshToken",
                ExpiryDate = DateTime.UtcNow.AddDays( 7 ),
                UserId = user.Id
            };
            _mockTokenService.Setup( t => t.GetUserIdFromExpiredToken( accessToken ) ).Returns( userId );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( userId ) ).ReturnsAsync( user );
            _mockUnitOfWork.Setup( u => u.authRepository.GetLastRefreshToken( userId ) ).ReturnsAsync( refreshToken );
            _mockTokenService.Setup( t => t.GenerateToken( user ) ).Returns( "newAccessToken" );

            // Act
            var result = await _userService.LoginByRefresh( accessToken, refreshToken.Token );

            // Assert
            Assert.That( result.Item1, Is.EqualTo( "newAccessToken" ) );
            Assert.That( result.Item2, Is.EqualTo( refreshToken.Token ) );
        }
        [Test]
        public void LoginByRefresh_ShouldThrowInvalidTokenException_WhenRefreshTokenIsInvalid() {
            // Arrange
            var userId = Guid.NewGuid();
            var accessToken = "expiredAccessToken";
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( userId, "testUser", hashedPassword, email );
            var invalidRefreshToken = new Auth.Entities.RefreshToken() {
                Id = Guid.NewGuid(),
                Token = "invalidRefreshToken",
                ExpiryDate = DateTime.UtcNow.AddDays( 7 ),
                UserId = user.Id
            };
            var validRefreshToken = new Auth.Entities.RefreshToken() {
                Id = Guid.NewGuid(),
                Token = "validRefreshToken",
                ExpiryDate = DateTime.UtcNow.AddDays( -7 ),
                UserId = user.Id
            };
            _mockTokenService.Setup( t => t.GetUserIdFromExpiredToken( accessToken ) ).Returns( userId );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( userId ) ).ReturnsAsync( user );
            _mockUnitOfWork.Setup( u => u.authRepository.GetLastRefreshToken( userId ) ).ReturnsAsync( validRefreshToken );

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidTokenException>( () => _userService.LoginByRefresh( accessToken, invalidRefreshToken.Token ) );
            Assert.That( ex.Message, Is.EqualTo( "Неверный токен!" ) );
        }
        [Test]
        public async Task Delete_ShouldCallDeleteUserAsync_AndSave() {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password";
            var refreshToken = "refreshToken";
            var token = "token";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( Guid.NewGuid(), userName, email, hashedPassword );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( user.Id ).Result ).Returns( user );
            _mockUnitOfWork.Setup( u => u.userRepository.DeleteAsync( user ) ).Returns( Task.CompletedTask );
            // Act
            await _userService.Delete( user.Id );

            // Assert
            _mockUnitOfWork.Verify( u => u.userRepository.DeleteAsync( user ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
        }
        [Test]
        public async Task GetUsers_ShouldReturnListOfUsers() {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( Guid.NewGuid(), "testUser", hashedPassword, email );
            var email2 = "test@example.com";
            var user2 = new User( Guid.NewGuid(), "testUser2", hashedPassword, email2 );
            var users = new List<User> { user, user2 };
            _mockUnitOfWork.Setup( u => u.userRepository.GetManyAsync( It.IsAny<int>(), It.IsAny<int>() ) ).ReturnsAsync( users );

            // Act
            var result = await _userService.GetUsers( 0, 10 );

            // Assert
            Assert.That( result, Is.EqualTo( users ) );
            Assert.That( result.Count() == 2 );
        }
        [Test]
        public async Task GetBooks_ShouldReturnListOfBooks() {
            // Arrange
            var userId = Guid.NewGuid();
            var books = new List<TakenBook> { new TakenBook(id: Guid.NewGuid(),
                title: "Harry Potter and the Prisoner of Azkaban",
                genre: "fantasy",
                description: "During the summer, Harry accidentally performs magic at the home of his Aunt Petunia and Uncle Vernon. After this incident, he leaves their house and spends the summer in London. While staying at the Leaky Cauldron inn, Harry is visited by Minister for Magic Cornelius Fudge, who warns him about Sirius Black, a mass-murderer who escaped from the wizard prison Azkaban.",
                ISBN:   "0747542155",
                authorId: Guid.NewGuid(),
                client_id: userId,
                takenAt: DateTime.UtcNow,
                returnTo: DateTime.UtcNow.AddDays( 1 )
                )};
            _mockUnitOfWork.Setup( u => u.userRepository.GetBooksAsync( userId, It.IsAny<int>(), It.IsAny<int>() ) ).ReturnsAsync( books );

            // Act
            var result = await _userService.GetBooks( userId, 0, 10 );

            // Assert
            Assert.That( result, Is.EqualTo( books ) );
        }

        [Test]
        public async Task Get_ShouldReturnUser() {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";
            var password = "password";
            var hashedPassword = _hasher.HashPassword( null, password );
            var user = new User( userId, "testUser", hashedPassword, email );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( userId ) ).ReturnsAsync( user );

            // Act
            var result = await _userService.Get( userId );

            // Assert
            Assert.That( result, Is.EqualTo( user ) );
        }

        [Test]
        public async Task Update_ShouldUpdateUser_WhenPasswordIsValid() {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@example.com";
            var userName = "userName";
            var password = "password";
            var passwordHash = _hasher.HashPassword( null, password );
            var user = new User( id: userId,
                userName: "testUser",
                passwordHash: passwordHash,
                email: email );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( userId ) ).ReturnsAsync( user );


            // Act
            await _userService.Update( userId, userName, email, password );

            // Assert
            _mockUnitOfWork.Verify( u => u.userRepository.UpdateAsync(
                It.Is<User>( u => u.Id == userId && u.UserName == userName
                && u.Email == email && u.PasswordHash == passwordHash ) ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );

        }
        [Test]
        public void Update_ShouldThrowValidationExceptionAndCallRollBack_WhenUpdatetUserInvalid() {
            // Arrange
            var userId = Guid.NewGuid();
            var invalidEmail = "invalid";
            var userName = "u";
            var password = "password";
            var passwordHash = _hasher.HashPassword( null, password );
            var user = new User( id: userId,
                userName: "testUser",
                passwordHash: passwordHash,
                email: "valid@mail.com" );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( userId ) ).ReturnsAsync( user );

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( () => _userService.Register( userName, invalidEmail, password ) );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
        }
    }
}