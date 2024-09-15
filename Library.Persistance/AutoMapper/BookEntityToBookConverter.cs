using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {

    public class BookEntityToBookConverter: ITypeConverter<BookEntity, Book> {

        public Book Convert( BookEntity source, Book destination, ResolutionContext context ) {
            if (source.BookType == BookType.Free) {
                return new FreeBook( source.Id,
                    source.Title,
                    source.Genre,
                    source.Description,
                    source.ISBN,
                    source.AuthorId);
            } else if (source.BookType == BookType.Taken) {
                return new TakenBook( source.Id,
                    source.ClientId.Value,
                    source.Title,
                    source.Genre,
                    source.Description,
                    source.ISBN,
                    source.AuthorId,
                    source.TakenAt.Value,
                    source.ReturnTo.Value );
            }

            throw new ArgumentException( "Invalid BookType" );
        }

    }
}
