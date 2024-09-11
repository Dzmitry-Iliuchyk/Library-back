using Library.Domain.Exceptions;

namespace Library.Domain.Models.Book {
    public class FreeBook: Book {
        public FreeBook(int id, string title, string genre, string description, string ISBN, Author author ) : base(id, title, genre, description, ISBN, author ) {
        }

        public FreeBook( Book book ) : base(book.Id, book.Title, book.Genre, book.Description, book.ISBN, book.Author ) {
        }

        public override TakenBook Take( int clientId, TimeSpan periodOfUse ) {
                return new TakenBook( this, clientId , DateTime.UtcNow, DateTime.UtcNow.Add(periodOfUse) );
        }
        public override FreeBook Free( int clientId ) {
           
            throw new BookAlreadyFreeException();
        }

        public override T Accept<T>( IBookVisitor<T> visitor ) {
            return visitor.Visit( this );
        }

        public override bool Equals( Book? other ) {
            return other is FreeBook freeBook
                  && freeBook.ISBN == ISBN
                  && freeBook.Id == Id
                  && freeBook.Author.Equals( Author )
                  && freeBook.Description == Description
                  && freeBook.Genre == Genre;
        }

        public override bool Equals( object? other ) {
            return other is Book book && Equals( book );
        }

        public override int GetHashCode() {
            return HashCode.Combine( 0, Id ,ISBN, Author, Description, Genre );
        }

    }
}