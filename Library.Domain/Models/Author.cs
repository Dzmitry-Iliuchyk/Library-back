using Library.Domain.Models.Book;

namespace Library.Domain.Models {
    public class Author: Entity, IEquatable<Author> {
        public Author( Guid id, string firstName, string lastName, DateTime birthday, string country, IEnumerable<Book.Book>? books = null ):base(id) {

            this.FirstName =  firstName;
            this.LastName = lastName  ;
            this.Birthday = birthday;
            this.Country = country;
            this.Books = books;
        }
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
