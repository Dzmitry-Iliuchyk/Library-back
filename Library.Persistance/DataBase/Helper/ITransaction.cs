using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DataBase.Helper {
    public interface ITransaction {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
