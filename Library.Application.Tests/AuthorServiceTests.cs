using FluentValidation;
using FluentValidation.Results;
using Library.Application.Exceptions;
using Library.Application.Implementations;
using Library.Application.Interfaces;
using Library.Application.Validator;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Moq;

namespace Library.Application.Tests {
    public class AuthorServiceTests {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IValidator<Author> _validator;
        private AuthorService _authorService;
        
        [SetUp]
        public void SetUp() {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _validator = new AuthorValidator();
            _authorService = new AuthorService( _mockUnitOfWork.Object, _validator );
        }

        [Test]
        public async Task CreateAuthorAsync_ValidAuthor_CallsAddAuthorAsync() {
            // Arrange
            var author = new Author( Guid.NewGuid(), "Teodor", "Potter", new DateTime( 1999, 1, 1 ), "Belarus" );
            _mockUnitOfWork.Setup( u => u.authorRepository.AddAuthorAsync( It.IsAny<Author>() ) ).Returns( Task.CompletedTask );

            // Act
            await _authorService.CreateAuthorAsync( author.FirstName, author.LastName, author.Birthday, author.Country );

            // Assert
            _mockUnitOfWork.Verify( u => u.authorRepository.AddAuthorAsync( It.Is<Author>( a => a.FirstName == "Teodor" && a.LastName == "Potter" ) ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
        }
        [Test]
        public void CreateAuthorAsync_InvalidAuthor_ThrowsValidationException() {
            // Arrange
            var author = new Author( Guid.NewGuid(), "a", "a", new DateTime( 1900, 1, 1 ), "a" );

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( async () =>await _authorService.CreateAuthorAsync( author.FirstName, author.LastName, author.Birthday, author.Country ) );
        }
        [Test]
        public async Task UpdateAuthorAsync_ValidAuthor_CallsUpdateAuthorAsync() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "Teodor", "Potter", new DateTime( 1999, 1, 1 ), "Belarus" );

            
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).Returns( Task.FromResult(author) );
            _mockUnitOfWork.Setup( u => u.authorRepository.UpdateAuthorAsync( It.IsAny<Author>() ) ).Returns( Task.CompletedTask );

            // Act
            await _authorService.UpdateAuthorAsync(author.Id, author.FirstName, author.LastName, author.Birthday, author.Country );

            // Assert
            _mockUnitOfWork.Verify( u => u.authorRepository.UpdateAuthorAsync( It.Is<Author>( a => a.FirstName == "Teodor" && a.LastName == "Potter" ) ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
        }
        [Test]
        public void UpdateAuthorAsync_InvalidAuthor_ThrowsValidationException() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "a", "a", new DateTime( 1900, 1, 1 ), "Be" );
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).Returns( Task.FromResult( author ) );

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>( async () => await _authorService.UpdateAuthorAsync(author.Id, author.FirstName, author.LastName, author.Birthday, author.Country ) );
        }
        [Test]
        public void DeleteAuthorAsync_AuthorHasBooks_ThrowsCannotDeleteAuthorWithBooksException() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "Joanne", "Rowling", new DateTime( 1965, 7, 31 ), "England", new List<Book> { new FreeBook(Guid.NewGuid(), "Harry Potter and the Prisoner of Azkaban", "fantasy",
                "During the summer, Harry accidentally performs magic at the home of his Aunt Petunia and Uncle Vernon. After this incident, he leaves their house and spends the summer in London. While staying at the Leaky Cauldron inn, Harry is visited by Minister for Magic Cornelius Fudge, who warns him about Sirius Black, a mass-murderer who escaped from the wizard prison Azkaban.",
                ISBN:   "0747542155", authorId )} );
                
            
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).ReturnsAsync( author );

            // Act & Assert
 
            var ex = Assert.ThrowsAsync<CannotDeleteAuthorWithBooksException>( () => _authorService.DeleteAuthorAsync( authorId ) );
            Assert.That( ex.Message, Is.EqualTo( "Перед удалением автора необходимо удалить все его книги" ) );

        }

        [Test]
        public async Task DeleteAuthorAsync_AuthorDoesNotHaveBooks_DeletesAuthor() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "Joanne", "Rowling", new DateTime( 1965, 7, 31 ), "England" );

            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).ReturnsAsync( author );
            _mockUnitOfWork.Setup( u => u.authorRepository.DeleteAuthorAsync( authorId ) ).Returns( Task.CompletedTask );

            // Act
            await _authorService.DeleteAuthorAsync( authorId );

            // Assert
            _mockUnitOfWork.Verify( u => u.CreateTransaction(), Times.Once );
            _mockUnitOfWork.Verify( u => u.authorRepository.DeleteAuthorAsync( authorId ), Times.Once );
            _mockUnitOfWork.Verify( u => u.Save(), Times.Once );
            _mockUnitOfWork.Verify( u => u.Commit(), Times.Once );
        }

        [Test]
        public void DeleteAuthorAsync_ExceptionOccurs_RollsBackTransaction() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "Joanne", "Rowling", new DateTime( 1965, 7, 31 ), "England");
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).ReturnsAsync( author );
            _mockUnitOfWork.Setup( u => u.authorRepository.DeleteAuthorAsync( authorId ) ).ThrowsAsync( new Exception() );

            // Act & Assert
            Assert.ThrowsAsync<Exception>( () => _authorService.DeleteAuthorAsync( authorId ) );
            _mockUnitOfWork.Verify( u => u.Rollback(), Times.Once );
        }

        [Test]
        public async Task GetAuthorsAsync_ShouldReturnAuthors() {
            // Arrange
            var authorId = Guid.NewGuid();
            var authors = new List<Author> { new Author( authorId, "Joanne", "Rowling", new DateTime( 1965, 7, 31 ), "England" ) };
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorsAsync( It.IsAny<int>(), It.IsAny<int>() ) ).ReturnsAsync( authors );

            // Act
            var result = await _authorService.GetAuthorsAsync( 0, 10 );

            // Assert
            Assert.AreEqual( authors, result );
        }

        [Test]
        public async Task GetBooksAsync_ShouldReturnBooks() {
            // Arrange
            var authorId = Guid.NewGuid();

            var books = new List<Book> { new FreeBook(Guid.NewGuid(), "Harry Potter and the Prisoner of Azkaban", "fantasy",
                "During the summer, Harry accidentally performs magic at the home of his Aunt Petunia and Uncle Vernon. After this incident, he leaves their house and spends the summer in London. While staying at the Leaky Cauldron inn, Harry is visited by Minister for Magic Cornelius Fudge, who warns him about Sirius Black, a mass-murderer who escaped from the wizard prison Azkaban.",
                ISBN:   "0747542155", authorId )};
            _mockUnitOfWork.Setup( u => u.authorRepository.GetBooksByAuthorAsync( authorId, It.IsAny<int>(), It.IsAny<int>() ) ).ReturnsAsync( books );

            // Act
            var result = await _authorService.GetBooksAsync( authorId, 0, 10 );

            // Assert
            Assert.That( result, Is.EqualTo( books ) );
        }

        [Test]
        public async Task GetAuthorAsync_ShouldReturnAuthor() {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author( authorId, "Joanne", "Rowling", new DateTime( 1965, 7, 31 ), "England" );
            _mockUnitOfWork.Setup( u => u.authorRepository.GetAuthorAsync( authorId ) ).ReturnsAsync( author );

            // Act
            var result = await _authorService.GetAuthorAsync( authorId );

            // Assert
            Assert.That( result, Is.EqualTo( author ) );
        }

    }
}