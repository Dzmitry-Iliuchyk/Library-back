using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {

    public class BookEntityToBookConverter: ITypeConverter<BookEntity, Book> {

        public Book Convert( BookEntity source, Book destination, ResolutionContext context ) {
            Author author = null; 
            if (source.Author != null) {
                author = new Author(id: source.Author.Id,
                    firstName: source.Author.FirstName,
                    lastName: source.Author.LastName,
                    birthday: source.Author.Birthday,
                    country: source.Author.Country);
            }
            User user = null; 
            if (source.User != null) {
                user = new User(id: source.User.Id,
                    userName: source.User.UserName,
                    passwordHash: source.User.PasswordHash,
                    email: source.User.Email);
            }
            
            if (source.BookType == BookType.Free) {
                return new FreeBook( id: source.Id,
                    title: source.Title,
                    genre: source.Genre,
                    description: source.Description,
                    ISBN: source.ISBN,
                    authorId: source.AuthorId,
                    author: author );
            } else if (source.BookType == BookType.Taken) {
                return new TakenBook(id: source.Id,
                    client_id: source.ClientId.Value,
                    title: source.Title,
                    genre: source.Genre,
                    description: source.Description,
                    ISBN: source.ISBN,
                    authorId: source.AuthorId,
                    takenAt: source.TakenAt.Value,
                    returnTo: source.ReturnTo.Value,
                    author: author,
                    client: user);
            }

            throw new ArgumentException( "Invalid BookType" );
        }

    }
}
