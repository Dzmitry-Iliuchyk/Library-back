namespace Library.Application.Exceptions {
    public class CannotDeleteAuthorWithBooksException: ApplicationException {
        public CannotDeleteAuthorWithBooksException() { }
        public CannotDeleteAuthorWithBooksException( string message ) : base( message ) { }
        public CannotDeleteAuthorWithBooksException( string message, Exception inner ) : base( message, inner ) { }
    }
}
