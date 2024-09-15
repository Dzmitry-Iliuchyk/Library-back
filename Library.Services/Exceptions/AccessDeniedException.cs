namespace Library.Application.Exceptions {
    public class AccessDeniedException: ApplicationException {
        public AccessDeniedException( string message ) : base( message ) { }
        public AccessDeniedException( string message, Exception inner ) : base( message, inner ) { }
    }
}
