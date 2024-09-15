namespace Library.DataAccess.Exceptions {
    public class CannotDeleteAuthorWithBooksException : DataAccessException{
        public CannotDeleteAuthorWithBooksException() { }
        public CannotDeleteAuthorWithBooksException( string message ) : base( message ) { }
        public CannotDeleteAuthorWithBooksException( string message, Exception inner ) : base( message, inner ) { }
    }
}
