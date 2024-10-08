﻿namespace Library.Domain.Models.Book {
    public abstract class Book: IEquatable<Book> {

        public Guid Id { get; private set; }
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public Guid AuthorId { get; private set; }
        public Author? Author { get; private set; }

        internal protected Book( Guid id, string title, string genre, string description, string ISBN, Guid authorId, Author? author = null ) {
            Id = id;
            this.ISBN = string.IsNullOrEmpty( ISBN ) ? throw new ArgumentNullException( nameof( ISBN ), "У книги должен быть ISBN" ) : ( ISBN );
            Title = string.IsNullOrEmpty( title ) ? throw new ArgumentNullException( nameof( title ), "У книги должен быть заголовок" ) : ( title );
            Genre = string.IsNullOrEmpty( genre ) ? throw new ArgumentNullException( nameof( genre ), "У книги должен быть жанр" ) : ( genre );
            Description = string.IsNullOrEmpty( description ) ? throw new ArgumentNullException( nameof( description ), "У книги должно быть описание" ) : ( description );
            AuthorId = authorId;
            this.Author = author;
        }

        public abstract TakenBook Take( Guid clientId, TimeSpan periodToUse );
        public abstract FreeBook Free( Guid clientId );
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
