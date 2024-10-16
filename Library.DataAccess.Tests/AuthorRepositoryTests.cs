using AutoMapper;
using Library.DataAccess.AutoMapper;
using Library.DataAccess.DataBase.Configuration;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Repository;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace Library.DataAccess.Tests {
    [TestFixture]
    public class AuthorRepositoryTests {
        private LibraryDBContext _context;
        private IMapper _mapper;
        private Mock<IOptions<AuthorizationOptions>> _mockOptions;
        private AuthorRepository _repository;
        private static (Guid, Guid) guids = (Guid.NewGuid(), Guid.NewGuid());
        private readonly IEnumerable<AuthorEntity> _authorEntities = new List<AuthorEntity>()
            {
                new AuthorEntity {
                    Id = guids.Item1,
                    FirstName = "Teodor",
                    LastName = "Potter",
                    Birthday = new DateTime( 1999, 1, 1 ),
                    Country = "Belarus",
                    Books =  [new BookEntity{
                Id= Guid.NewGuid(),
                ISBN= "1234567890",
                Title= "Test Title",
                Genre= "Test Genre",
                Description= "Test DescriptionTest DescriptionTest DescriptionTest Description",
                AuthorId= guids.Item1 },new BookEntity{
                Id= Guid.NewGuid(),
                ISBN= "1236566890",
                Title= "Test Title2",
                Genre= "Test Genre2",
                Description= "Test Descript2ionTest Descr2iptionTest Description2Test Description",
                AuthorId= guids.Item1 } ]
                },

            new AuthorEntity {
                Id = guids.Item2,
                FirstName = "Albus",
                LastName = "Dambldor",
                Birthday = new DateTime( 1929, 1, 1 ),
                Country = "England"
            },
        };
        private readonly IEnumerable<Author> _authors = new List<Author>()
            {
                new Author (
                    id : guids.Item1,
                    firstName: "Teodor",
                    lastName: "Potter",
                    birthday: new DateTime( 1999, 1, 1 ),
                    country: "Belarus"
                ),
            new Author (
                id: guids.Item2,
                firstName:  "Albus",
                lastName:  "Dambldor",
                birthday: new DateTime( 1929, 1, 1 ),
                country: "England"
            ),
        };

        [SetUp]
        public void Setup() {
            _mapper = Helpers.ConfigureMapper();
            _mockOptions = new Mock<IOptions<AuthorizationOptions>>();
            _mockOptions.Setup( o => o.Value ).Returns( Helpers.AuthOptions() );
            _context = new LibraryDBContext( new DbContextOptionsBuilder<LibraryDBContext>().UseInMemoryDatabase( "TestDb" ).Options, _mockOptions.Object );
            _context.Database.EnsureDeleted();
            _context.Authors.AddRange( _authorEntities );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            _repository = new AuthorRepository( _context, _mapper );
        }
        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }
        [Test]
        public async Task AddAuthorAsync_ShouldAddedAuthor() {
            //Arrange
            var authorId = Guid.NewGuid();
            var author = new Author(
                id: authorId,
                firstName: "Merry",
                lastName: "Darkstone",
                birthday: new DateTime( 1949, 1, 1 ),
                country: "England" );


            var authorEntity = new AuthorEntity {
                Id = authorId,
                FirstName = "Merry",
                LastName = "Darkstone",
                Birthday = new DateTime( 1949, 1, 1 ),
                Country = "England"
            };

            //Act
            await _repository.CreateAsync( author );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( GetAuthorFromContext( x => x.FirstName == author.FirstName ).Id, Is.EqualTo( authorEntity.Id ) );
        }

        [Test]
        public async Task UpdateAuthorAsync_ShouldUpdateAuthor() {

            var updatedAuthor = new Author(
                id: guids.Item2,
                firstName: "Updated Author",
                lastName: "Darkstone",
                birthday: new DateTime( 1949, 1, 1 ),
                country: "England"
            );
            //Act
            await _repository.UpdateAsync( updatedAuthor );
            await _context.SaveChangesAsync();
            var updatedAuthorInDb = GetAuthorFromContext( x => x.Id == guids.Item2 );
            //Assert
            Assert.That( updatedAuthorInDb.FirstName, Is.EqualTo( updatedAuthor.FirstName ) );
            Assert.That( updatedAuthorInDb.LastName, Is.EqualTo( updatedAuthor.LastName ) );


        }
        [Test]
        public async Task DeleteAuthorAsync_ShouldDeleteAuthor() {
            //Act
            await _repository.DeleteAsync( _authors.First() );
            await _context.SaveChangesAsync();
            //Assert
            Assert.That( _context.Authors.Count(), Is.EqualTo( 1 ) );
        }

        [Test]
        public async Task GetAuthorsAsync_ShouldReturnAuthors() {
            //Act
            var result = await _repository.GetManyAsync( 0, 2 );
            //Assert
            Assert.That( result.Count, Is.EqualTo( 2 ) );
            Assert.That( result.FirstOrDefault( x => x.FirstName == _authors.ElementAt( 1 ).FirstName ).Id, Is.EqualTo( _authors.ElementAt( 1 ).Id ) );
        }
        [Test]
        public async Task GetAuthorAsync_ShouldReturnAuthor() {
            //Act
            var result = await _repository.GetAuthorWithBooksAsync( guids.Item2 );
            //Assert
            Assert.That( result, Is.EqualTo( _authors.ElementAt(1) ) );
           
        }

        [Test]
        public async Task GetBooksByAuthorAsync_ShouldReturnBooks() {
            //Act
            var result = await _repository.GetBooksByAuthorAsync(guids.Item1,0,5);
            //Assert
            Assert.AreEqual( 2, result.Count );
            Assert.IsTrue( result.Any(x=>x.Title == "Test Title2"));
        }
        private AuthorEntity? GetAuthorFromContext( System.Linq.Expressions.Expression<Func<AuthorEntity, bool>> predicate ) {
            return this._context.Authors.AsNoTracking().FirstOrDefault( predicate );
        }

    }
}