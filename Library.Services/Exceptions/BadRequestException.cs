namespace Library.Application.Exceptions {
    public class BadRequestException: ApplicationException {
        public BadRequestException( string message ) : base( message ) { }
        public BadRequestException( string message, Exception inner ) : base( message, inner ) { }
    }
}
