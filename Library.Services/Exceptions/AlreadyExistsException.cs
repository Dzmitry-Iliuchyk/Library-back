namespace Library.Application.Exceptions {
    public class AlreadyExistsException: ApplicationException {
        public AlreadyExistsException( string message ) : base( message ) { }
        public AlreadyExistsException( string message, Exception inner ) : base( message, inner ) { }
    }
}
