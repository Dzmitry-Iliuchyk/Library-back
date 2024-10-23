using Library.Application.Interfaces.Repositories;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.DataAccess.Repository
{
    public abstract class BaseRepository<TDbEntity, TDomainEntity>: IRepository<TDomainEntity>
        where TDbEntity : DbEntity
        where TDomainEntity : Entity {
        private protected readonly DbSet<TDbEntity> _dbSet;

        protected abstract TDbEntity MapToDBEntity( TDomainEntity domainEntity );
        protected abstract TDomainEntity MapToDomainEntity( TDbEntity dbEntity );
        protected abstract IList<TDomainEntity> MapToDomainEntities( IList<TDbEntity> dbEntities );
        public BaseRepository( DbContext dbContext ) {
            this._dbSet = dbContext.Set<TDbEntity>();
        }

        public virtual async Task CreateAsync( TDomainEntity entity ) {
            var dbEntity = MapToDBEntity( entity );
            await _dbSet.AddAsync( dbEntity );
        }

        public virtual async Task DeleteAsync( TDomainEntity entity ) {
            var dbEntity = await _dbSet.FirstOrDefaultAsync( x => x.Id == entity.Id );
            if (dbEntity != null) {
                _dbSet.Remove( dbEntity );
            }
        }

        public virtual async Task<IList<TDomainEntity>> GetManyAsync( int skip = 0, int take = 0 ) {
            var query = _dbSet
               .AsNoTracking();
            if (skip > 0) {
                query.Skip( skip );
            }
            if (take > 0) {
                query.Take( take );
            }
            var entities = await query.ToListAsync();
            return MapToDomainEntities( entities );
        }

        public virtual async Task<TDomainEntity> GetAsync( Guid id ) {
            var userEntity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync( x => x.Id == id );

            return MapToDomainEntity( userEntity );
        }

        public virtual async Task UpdateAsync( TDomainEntity entity ) {
            var dbEntity = MapToDBEntity( entity );
            _dbSet.Update( dbEntity );
        }
        public virtual async Task<bool> Exist(Guid id) {
            return await _dbSet.AsNoTracking().AnyAsync( x=>x.Id == id );
        }
    }
}
