namespace Library.Domain.Models.Book
{
    public abstract class Book: Entity, IEquatable<Book> { 
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public Guid AuthorId { get; private set; }
        public Author? Author { get; private set; }

        internal protected Book( Guid id, string title, string genre, string description, string ISBN, Guid authorId, Author? author = null ) : base(id){
            Title = title;
            Genre = genre;
            Description = description;
            this.ISBN = ISBN;
            AuthorId = authorId;
            Author = author;
        }

        public abstract bool Equals( Book? other );
        public abstract override bool Equals( object? other );
        public abstract override int GetHashCode();

        public static bool operator ==( Book? left, Book? right ) {
            if (left is null) {
                return right is null;
            }

            return left.Equals( right );
        }

        public static bool operator !=( Book? left, Book? right ) {
            return !( left == right );
        }
    }
}
