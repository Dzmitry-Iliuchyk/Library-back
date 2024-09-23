using FluentValidation;
using Library.Application.Implementations;
using Library.Application.Interfaces;
using Library.Application.Validator;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models.Book;
using Moq;

namespace Library.Application.Tests {
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

            _mockUnitOfWork.Setup( u => u.bookRepository.CreateBookAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.CreateBookAsync( book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId );

            // Assert
            _mockUnitOfWork.Verify( u => u.bookRepository.CreateBookAsync( It.IsAny<Book>() ), Times.Once );
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

            _mockUnitOfWork.Setup( u => u.bookRepository.CreateBookAsync( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

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
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.UpdateBookAsync( bookId, book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId );

            // Assert

            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ), Times.Once );
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
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( () => _bookService.UpdateBookAsync( bookId, book.ISBN, book.Title, book.Genre, book.Description, book.AuthorId ) );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
        }
        [Test]
        public async Task DeleteBookAsync_ShouldDeleteBook() {
            // Arrange
            var bookId = Guid.NewGuid();
            _mockUnitOfWork.Setup( u => u.bookRepository.DeleteBookAsync( bookId ) ).Returns( Task.CompletedTask );
            _mockImageService.Setup( i => i.DeleteImage( bookId ) ).Verifiable();

            // Act
            await _bookService.DeleteBookAsync( bookId );

            // Assert
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.DeleteBookAsync( bookId ), Times.Once );
            _mockImageService.Verify( i => i.DeleteImage( bookId ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }

        [Test]
        public void DeleteBookAsync_OccursException_ShouldCallRollback() {
            // Arrange
            var bookId = Guid.Empty;
            _mockUnitOfWork.Setup( u => u.bookRepository.DeleteBookAsync( bookId ) ).Throws( new Exception() );
            _mockImageService.Setup( i => i.DeleteImage( bookId ) ).Verifiable();

            //Act & Assert
            Assert.ThrowsAsync<Exception>( () => _bookService.DeleteBookAsync( bookId ) );
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
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.FreeBookAsync( bookId, clientId );

            // Assert
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookAsync( bookId ), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
        }

        [Test]
        public async Task GiveBookToClientAsync_ShouldGiveBookToClient() {
            // Arrange
            var bookId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var book = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: Guid.NewGuid() );
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );

            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act
            await _bookService.GiveBookToClientAsync( bookId, clientId, 48 );

            // Assert
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookAsync( bookId ), Times.Once );
            _mockUnitOfWork.Verify( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }
        [Test]
        public void GiveBookToClientAsync_OccursBookTakenException_ShouldCallRollback() {
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
            
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );
            _mockUnitOfWork.Setup( u => u.bookRepository.UpdateBook( It.IsAny<Book>() ) ).Returns( Task.CompletedTask );

            // Act & Assert
            Assert.ThrowsAsync<BookTakenException>( () => _bookService.GiveBookToClientAsync( bookId, clientId, 48 ) );
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
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
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( bookId ) ).ReturnsAsync( book );

            // Act
            var result = await _bookService.GetBookAsync( bookId );

            // Assert
            Assert.That( result, Is.EqualTo( book ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookAsync( bookId ), Times.Once );
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
            _mockUnitOfWork.Setup( u => u.bookRepository.GetBookAsync( ISBN ) ).ReturnsAsync( book );

            // Act
            var result = await _bookService.GetBookAsync( ISBN );

            // Assert
            Assert.That( result, Is.EqualTo( book ) );
            _mockUnitOfWork.Verify( u => u.bookRepository.GetBookAsync( ISBN ), Times.Once );
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