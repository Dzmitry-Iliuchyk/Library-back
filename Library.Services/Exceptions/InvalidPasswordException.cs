namespace Library.Application.Exceptions {
    public class InvalidPasswordException: ApplicationException {
        public InvalidPasswordException( string message ) : base( message ) { }
        public InvalidPasswordException( string message, Exception inner ) : base( message, inner ) { }
    }
}
