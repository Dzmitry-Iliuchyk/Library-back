using FluentValidation;
using Library.Application.Implementations;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Validator;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Moq;

namespace Library.Application.Tests
{
    public class BookServiceTests {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IImageService> _mockImageService;
        private IValidator<Book> _validator;

        private BookService _bookService;
        [SetUp]
        public void SetUp() {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockImageService = new Mock<IImageService>();
            _validator = new BookValidator();
            _bookService = new BookService( _mockUnitOfWork.Object, _validator, _mockImageService.Object );
        }
        [Test]
        public async Task CreateBookAsync_WhenDataValid_ShouldCreateBook() {
            // Arrange
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() );

            _mockUnitOfWork.Setup( u => u.bookRepository.CreateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.CreateBookAsync( book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId );

            // Assert
            _mockUnitOfWork.Verify( u => u.bookRepository.CreateAsync( It.IsAny<Book>() ), Times.Once );
        }
        [Test]
        public void CreateBookAsync_WhenDataInValid_ShouldThrowValidationException() {
            // Arrange
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234",
                title: "T",
                genre: "T",
                description: "Test",
                authorId: Guid.NewGuid() );

            _mockUnitOfWork.Setup( u => u.bookRepository.CreateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            //Act & Assert
            Assert.ThrowsAsync<ValidationException>( () => _bookService.CreateBookAsync( book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId ) );

        }
        [Test]
        public async Task UpdateBookAsync_ShouldUpdateBook_CallCreateTransactionAndCommit() {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new FreeBook(
               id: bookId,
               ISBN: "1234567890",
               title: "Test Title",
               genre: "Test Genre",
               description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
               authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAuthorAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.UpdateBookAsync( bookId, book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId );

            // Assert

            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }
        [Test]
        public void UpdateBookAsync_ShouldThrowException_CallRollBack() {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new FreeBook(
               id: bookId,
               ISBN: "56790",
               title: "Te",
               genre: "T",
               description: "Test",
               authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAuthorAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( () => _bookService.UpdateBookAsync( bookId, book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId ) );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
        }
        [Test]
        public async Task DeleteBookAsync_ShouldDeleteBook() {
            // Arrange
            var book = new FreeBook(
               id: Guid.NewGuid(),
               ISBN: "1234567890",
               title: "Test Title",
               genre: "Test Genre",
               description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
               authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.DeleteAsync(book) ).Returns( Task.CompletedTask );
            _mockImageService.Setup( i => i.DeleteImage( book.Id ) ).Verifiable();

            // Act
            await _bookService.DeleteBookAsync( book.Id );

            // Assert
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.DeleteAsync(It.IsAny<Book>()), Times.Once );
            _mockImageService.Verify( i => i.DeleteImage( book.Id ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }

        [Test]
        public async Task DeleteBookAsync_OccursException_ShouldCallRollback() {
            // Arrange
            var book = new FreeBook(
               id: Guid.NewGuid(),
               ISBN: "1234567890",
               title: "Test Title",
               genre: "Test Genre",
               description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
               authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.DeleteAsync(book) ).Verifiable( );
            _mockImageService.Setup( i => i.DeleteImage( book.Id ) ).Throws(new Exception());

            //Act & Assert
            Assert.ThrowsAsync<Exception>( () => _bookService.DeleteBookAsync( book.Id )) ;
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );

        }
        [Test]
        public async Task FreeBookAsync_ShouldFreeBook() {
            // Arrange
            var bookId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var book = new TakenBook( id: bookId,
                client_id: clientId,
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test Description",
                authorId: Guid.NewGuid(),
                takenAt: DateTime.Now,
                returnTo: DateTime.Now.AddDays( 7 ) );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAllAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.FreeBookAsync( bookId, clientId );

            // Assert
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookWithAllAsync( bookId ), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
        }

        [Test]
        public async Task GiveBookToClientAsync_ShouldGiveBookToClient() {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password";
            var refreshToken = "refreshToken";
            var token = "token";
            var hashedPassword = "papdsapd";
            var user = new User( Guid.NewGuid(), userName, email, hashedPassword );
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetAsync( book.Id ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( user.Id ) ).ReturnsAsync( user );

            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.GiveBookToClientAsync( book.Id, user.Id, 48 );

            // Assert
            _mockUnitOfWork.Verify( u => u.bookRepository.GetAsync( book.Id ), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );

        }
        [Test]
        public async Task GiveTakenBookToClientAsync_ShouldNotCallUpdate() {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password";
            var refreshToken = "refreshToken";
            var token = "token";
            var hashedPassword = "papdsapd";
            var user = new User( Guid.NewGuid(), userName, email, hashedPassword );
            var book = new TakenBook(
                id: Guid.NewGuid(),
                client_id: user.Id,
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                takenAt: DateTime.UtcNow,
                returnTo: DateTime.UtcNow.AddDays(3),
                authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetAsync( book.Id ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.userRepository.GetAsync( user.Id ) ).ReturnsAsync( user );

            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act & Assert
            await _bookService.GiveBookToClientAsync( book.Id, user.Id, 48 );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateAsync( It.IsAny<Book>() ), Times.Never );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Never );
        }
        [Test]
        public async Task GetBooksAsync_ShouldReturnBooks() {
            // Arrange
            var books = new List<Book> { new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() ) };
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBooksAsync( It.IsAny<int>(), It.IsAny<int>() ) ).ReturnsAsync( books );

            // Act
            var result = await _bookService.GetBooksAsync( 0, 10 );

            // Assert
            Assert.That( result, Is.EqualTo( books ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBooksAsync( 0, 10 ), Times.Once );
        }

        [Test]
        public async Task GetBookAsync_ById_ShouldReturnBook() {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAuthorAsync( bookId ) ).ReturnsAsync( book );

            // Act
            var result = await _bookService.GetBookWithAuthorAsync( bookId );

            // Assert
            Assert.That( result, Is.EqualTo( book ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookWithAuthorAsync( bookId ), Times.Once );
        }

        [Test]
        public async Task GetBookAsync_ByISBN_ShouldReturnBook() {
            // Arrange
            var ISBN = "1234567890";
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: ISBN,
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAuthorAsync( ISBN ) ).ReturnsAsync( book );

            // Act
            var result = await _bookService.GetBookAsync( ISBN );

            // Assert
            Assert.That( result, Is.EqualTo( book ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookWithAuthorAsync( ISBN ), Times.Once );
        }
        [Test]
        public async Task GetFilteredBooksAsync_ShouldInvokeRepo() {
            // Arrange
            var authorId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var ISBN = "1234567890";
            var book = new FreeBook(
                id: userId,
                ISBN: ISBN,
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: authorId,
                author: new Domain.Models.Author( authorId, "test", "test", DateTime.Now.AddYears( -15 ), "test" ) );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetFilteredBooksAsync(0,1, "test", "test" ) ).ReturnsAsync( ([book], 1)  );

            // Act
            var result = await _bookService.GetFilteredBooksAsync( 0, 1, "test", "test" );

            // Assert
            Assert.That( result.Item1.First(), Is.EqualTo( book ));
            _mockUnitOfWork.Verify( u => u.bookRepository.GetFilteredBooksAsync( 0, 1, "test", "test" ), Times.Once );
        }
        [Test]
        public async Task GetBookWithAllAsync_ShouldInvokeRepository() {
            // Arrange
            var authorId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var ISBN = "1234567890";
            var book = new FreeBook(
                id: userId,
                ISBN: ISBN,
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: authorId,
                author: new Domain.Models.Author( authorId, "test", "test", DateTime.Now.AddYears(-15),"test" ) );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookWithAllAsync( userId ) ).ReturnsAsync( book );

            // Act
            var result = await _bookService.GetBookWithAllAsync( userId );

            // Assert
            Assert.That( result, Is.EqualTo( book ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookWithAllAsync( userId ), Times.Once );
        }
    }
}