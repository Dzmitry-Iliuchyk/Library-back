using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Exceptions {
    public class UserNotFoundException : DataAccessException {
        public override string Message => "Такого пользователя не существует!";
    }

}
