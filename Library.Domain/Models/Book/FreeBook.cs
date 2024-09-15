using Library.Domain.Exceptions;

namespace Library.Domain.Models.Book {
    public class FreeBook: Book {
        public FreeBook( Guid id, string title, string genre, string description, string ISBN, Guid authorId )
            : base(id, title, genre, description, ISBN, authorId) {
        }
        public FreeBook( Book book ) : base(book.Id, book.Title, book.Genre, book.Description, book.ISBN, book.AuthorId ) {
        }

        public override TakenBook Take( Guid clientId, TimeSpan periodOfUse ) {
                return new TakenBook( this, clientId , DateTime.UtcNow, DateTime.UtcNow.Add(periodOfUse) );
        }
        public override FreeBook Free( Guid clientId ) {
           
            throw new BookAlreadyFreeException();
        }
        public override bool Equals( Book? other ) {
            return other is FreeBook freeBook
                  && freeBook.ISBN == ISBN
                  && freeBook.Id == Id
                  && freeBook.AuthorId == AuthorId
                  && freeBook.Description == Description
                  && freeBook.Genre == Genre;
        }

        public override bool Equals( object? other ) {
            return other is Book book && Equals( book );
        }

        public override int GetHashCode() {
            return HashCode.Combine( 0, Id ,ISBN, AuthorId, Description, Genre );
        }

    }
}