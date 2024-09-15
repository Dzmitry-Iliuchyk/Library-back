namespace Library.Application.Exceptions {
    public class ItemNotFoundException: ApplicationException {
        public ItemNotFoundException( string message ) : base( message ) { }
        public ItemNotFoundException( string message, Exception inner ) : base( message, inner ) { }
    }
}
