namespace Library.Application.Exceptions {
    public class InvalidTokenException: ApplicationException {
        public InvalidTokenException( string message ) : base( message ) { }
        public InvalidTokenException( string message, Exception inner ) : base( message, inner ) { }
    }
}
