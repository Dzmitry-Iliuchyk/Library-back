using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {

    public class BookEntityToBookConverter: ITypeConverter<BookEntity, Book> {

        public Book Convert( BookEntity source, Book destination, ResolutionContext context ) {
            if (source.BookType == BookType.Free) {
                return new FreeBook(id: source.Id,
                    title: source.Title,
                    genre: source.Genre,
                    description: source.Description,
                    ISBN: source.ISBN,
                    authorId: source.AuthorId);
            } else if (source.BookType == BookType.Taken) {
                return new TakenBook(id: source.Id,
                    client_id: source.ClientId.Value,
                    title: source.Title,
                    genre: source.Genre,
                    description: source.Description,
                    ISBN: source.ISBN,
                    authorId: source.AuthorId,
                    takenAt: source.TakenAt.Value,
                    returnTo: source.ReturnTo.Value );
            }

            throw new ArgumentException( "Invalid BookType" );
        }

    }
}
