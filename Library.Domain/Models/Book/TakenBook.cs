using Library.Domain.Exceptions;

namespace Library.Domain.Models.Book {
    public class TakenBook: Book {
        public Guid ClientId { get; private set; }
        public DateTime TakenAt { get; private set; }
        public DateTime ReturnTo { get; private set; }

        public TakenBook( Guid id, Guid client_id, string title, string genre, string description, string ISBN, Guid authorId, DateTime takenAt, DateTime returnTo ) 
            : base(id, title, genre, description, ISBN, authorId ) {
            ClientId = client_id;
            TakenAt = takenAt;
            ReturnTo = returnTo;
        }
        public TakenBook( FreeBook book, Guid client_id, DateTime takenAt, DateTime returnTo ) 
            : base(book.Id, book.Title, book.Genre, book.Description, book.ISBN, book.AuthorId ) {
            ClientId = client_id;
            TakenAt = takenAt;
            ReturnTo = returnTo;
        }
        public override TakenBook Take( Guid clientId , TimeSpan periodOfUse ) {
            throw new BookTakenException( ClientId );
        }
        public override FreeBook Free( Guid clientId ) {
            if (ClientId == clientId) {
                return new FreeBook( this );
            }

            throw new BookFreeException();
        }
        public override bool Equals( Book? other ) {
            return other is TakenBook takenBook 
                  && takenBook.ISBN == ISBN 
                  && takenBook.ClientId == ClientId
                  && takenBook.Id == Id
                  && takenBook.AuthorId == AuthorId
                  && takenBook.Description == Description
                  && takenBook.Genre == Genre;
        }

        public override bool Equals( object? other ) {
            return other is Book book && Equals( book );
        }

        public override int GetHashCode() {
            return HashCode.Combine( 1,Id, ISBN, ClientId, AuthorId, Description, Genre );
        }
    }
}