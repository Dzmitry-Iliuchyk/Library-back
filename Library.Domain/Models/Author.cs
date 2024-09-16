using Library.Domain.Models.Book;

namespace Library.Domain.Models {
    public class Author: IEquatable<Author> {
        public Author( Guid id, string firstName, string lastName, DateTime birthday, string country, IEnumerable<Book.Book>? books = null ) {
            Id = id;
            this.FirstName = string.IsNullOrEmpty( firstName ) ? throw new ArgumentNullException( nameof( firstName ) ): firstName;
            this.LastName = string.IsNullOrEmpty( lastName ) ? throw new ArgumentNullException( nameof( lastName ) ) :lastName  ;
            this.Birthday = birthday;
            this.Country = string.IsNullOrEmpty( country ) ? throw new ArgumentNullException( nameof( country ) ) : country;
            this.Books = books;
        }
        private Author() { }
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthday { get; private set; }

        public string Country { get; private set; }

        public IEnumerable< Book.Book>? Books { get; private set; }

        public bool Equals( Author? other ) {
            return other is not null
                && other.Id == Id 
                && other.FirstName == FirstName 
                && other.LastName == LastName
                && other.Birthday == Birthday 
                && other.Country == Country;
        }

        public override bool Equals( object? other ) {
            return other is Author author && Equals( author );
        }

        public override int GetHashCode() {
            return HashCode.Combine( Id, FirstName, LastName, Country, Books );
        }
    }
}
