namespace Library.Application.Exceptions {
    public class FileWriteException: ApplicationException {

        public FileWriteException( string message ) : base( message ) { }
        public FileWriteException( string message, Exception inner ) : base( message, inner ) { }
    }
}
