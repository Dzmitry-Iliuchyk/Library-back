

namespace Library.Domain.Models.Book {
    public class TakenBook: Book {
        public Guid ClientId { get; private set; }
        public User? Client {  get; private set; }
        public DateTime TakenAt { get; private set; }
        public DateTime ReturnTo { get; private set; }

        public TakenBook( Guid id, Guid client_id, string title, string genre, string description, string ISBN, Guid authorId, DateTime takenAt, DateTime returnTo, Author? author = null, User? client = null )
            : base( id, title, genre, description, ISBN, authorId, author ) {
            ClientId = client_id;
            TakenAt = takenAt;
            ReturnTo = returnTo;
            this.Client = client;
        }
        public TakenBook( FreeBook book, Guid client_id, DateTime takenAt, DateTime returnTo, User? client = null ) 
            : base(book.Id, book.Title, book.Genre, book.Description, book.ISBN, book.AuthorId, book.Author ) {
            ClientId = client_id;
            TakenAt = takenAt;
            ReturnTo = returnTo;
            this.Client = client;
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