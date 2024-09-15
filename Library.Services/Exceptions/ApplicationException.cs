using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions {
    public class ApplicationException : Exception{
        public ApplicationException(string message): base(message) { }
        public ApplicationException(string message, Exception inner): base(message, inner) { }
    }
}
