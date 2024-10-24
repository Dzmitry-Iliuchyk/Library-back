﻿namespace Library.Application.Exceptions {
    public class NotFoundException: ApplicationException {
        public NotFoundException( string message ) : base( message ) { }
        public NotFoundException( string message, Exception inner ) : base( message, inner ) { }
    }
}
