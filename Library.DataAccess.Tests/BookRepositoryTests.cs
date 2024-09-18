using AutoMapper;
using Library.DataAccess.DataBase.Configuration;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Repository;
using Library.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace Library.DataAccess.Tests {
    [TestFixture]
    public class BookRepositoryTests {
        private LibraryDBContext _context;
        private IMapper _mapper;
        private Mock<IOptions<AuthorizationOptions>> _mockOptions;
        private BookRepository _repository;
        private static (Guid, Guid) guidsAuthors = (Guid.NewGuid(), Guid.NewGuid());
        private static (Guid, Guid) guidsBooks = (Guid.NewGuid(), Guid.NewGuid());
        private readonly IEnumerable<BookEntity> _bookEntities = new List<BookEntity>()
            {
            new BookEntity{
                Id= guidsBooks.Item1,
                ISBN= "1234567890",
                Title= "Test Title",
                Genre= "Test Genre",
                Description= "Test DescriptionTest DescriptionTest DescriptionTest Description",
                Author = new AuthorEntity {
                    Id = guidsAuthors.Item1,
                    FirstName = "Teodor",
                    LastName = "Potter",
                    Birthday = new DateTime( 1999, 1, 1 ),
                    Country = "Belarus"
                }
            },
            new BookEntity{
                Id= guidsBooks.Item2,
                ISBN= "1236566890",
                Title= "Test Title2",
                Genre= "Test Genre2",
                Description= "Test Descript2ionTest Descr2iptionTest Description2Test Description",
                Author =  new AuthorEntity {
                    Id = guidsAuthors.Item2,
                    FirstName = "Albus",
                    LastName = "Dambldor",
                    Birthday = new DateTime( 1929, 1, 1 ),
                    Country = "England"
                }
            }
        };
        private readonly IEnumerable<Book> _books = new List<Book>()
            {
                new FreeBook(
                id: guidsBooks.Item1,
                ISBN: "1234567890",
                title: "Test Title",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: guidsAuthors.Item1)
            ,
            new FreeBook(
                id:guidsBooks.Item2 ,
                ISBN: "1236566890",
                title: "Test Title2",
                genre: "Test Genre2",
                description: "Test Descript2ionTest Descr2iptionTest Description2Test Description",
                authorId: guidsAuthors.Item2)

        };

        [SetUp]
        public void Setup() {
            _mapper = Helpers.ConfigureMapper();
            _mockOptions = new Mock<IOptions<AuthorizationOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( Helpers.AuthOptions() );
            _context = new LibraryDBContext( new DbContextOptionsBuilder<LibraryDBContext>().UseInMemoryDatabase( "TestDb" ).Options, _mockOptions.Object );
            _context.Database.EnsureDeleted();
            _context.Books.AddRange( _bookEntities );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            _repository = new BookRepository( _context, _mapper );

        }

        [Test]
        public async Task CreateBookAsync_ShouldCreateBook() {
            //Arrange
            var newBook = new FreeBook(
                id: Guid.NewGuid(),
                ISBN: "1234567890",
                title: "NewBook1",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: guidsAuthors.Item1 );

            //Act
            await _repository.CreateBookAsync( newBook );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.Books.FirstOrDefault( x => x.Title == newBook.Title ).Title, Is.EqualTo( newBook.Title ) );
        }

        [Test]
        public async Task UpdateBook_ShouldUpdateBook() {
            //Arrange

            var newBook = new FreeBook(
                id: guidsBooks.Item1,
                ISBN: "1234567890",
                title: "UpdatedBook",
                genre: "Test Genre",
                description: "Test DescriptionTest DescriptionTest DescriptionTest Description",
                authorId: guidsAuthors.Item1 );
            //Act
            await _repository.UpdateBook( newBook );
            await _context.SaveChangesAsync();
            var updatedBookInDb = _context.Books.First( x => x.Id == guidsBooks.Item1 );
            //Assert
            Assert.That( updatedBookInDb.Title, Is.EqualTo( newBook.Title ) );
        }
        [Test]
        public async Task DeleteAuthorAsync_ShouldDeleteAuthor() {
            //Act
            await _repository.DeleteBookAsync( guidsBooks.Item2 );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.Books.Count(), Is.EqualTo( 1 ) );
        }
        [Test]
        public async Task GetBooksAsync_ShouldReturnBooks() {
            //Act
            var result = await _repository.GetBooksAsync( 0, 2 );
            //Assert
            Assert.That( _context.Books.Count(), Is.EqualTo( 2 ) );
        }

        [Test]
        public async Task GetBookAsync_inputGuid_ShouldReturnBook() {
            //Act
            var result = await _repository.GetBookAsync( guidsBooks.Item1 );
            //Assert
            Assert.That( result, Is.EqualTo( _books.First( x => x.Id == guidsBooks.Item1 ) ) );
        }
        [Test]
        public async Task GetBookAsync_inputISBN_ShouldReturnBook() {
            //Arrange
            var book = _books.First( x => x.Id == guidsBooks.Item1 );
            //Act
            var result = await _repository.GetBookAsync( book.ISBN );
            //Assert
            Assert.That( result, Is.EqualTo( _books.First( x => x.Id == guidsBooks.Item1 ) ) );
        }
        

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }
    }
}