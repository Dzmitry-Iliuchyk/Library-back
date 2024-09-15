namespace Library.Application.Exceptions {
    public class FileWriteException: ApplicationException {

        public FileWriteException( string message ) : base( message ) { }
        public FileWriteException( string message, Exception inner ) : base( message, inner ) { }
    }
    public class ItemNotFoundException: ApplicationException {
        public ItemNotFoundException( string message ) : base( message ) { }
        public ItemNotFoundException( string message, Exception inner ) : base( message, inner ) { }
    }
    public class AccessDeniedException: ApplicationException {
        public AccessDeniedException( string message ) : base( message ) { }
        public AccessDeniedException( string message, Exception inner ) : base( message, inner ) { }
    }
}
