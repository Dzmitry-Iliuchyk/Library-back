using Microsoft.EntityFrameworkCore.Storage;

namespace Library.DataAccess.DataBase.Helper {
    public class EfTransaction: ITransaction {
        private IDbContextTransaction _dbContextTransaction;
        public EfTransaction( IDbContextTransaction dbContextTransaction ) {
            _dbContextTransaction = dbContextTransaction;
        }
        public async Task CommitAsync() =>await _dbContextTransaction.CommitAsync();
        public async Task RollbackAsync() =>await _dbContextTransaction.RollbackAsync();
        public void Dispose() {
            _dbContextTransaction?.Dispose();
            _dbContextTransaction = null!;
        }
    }
}
