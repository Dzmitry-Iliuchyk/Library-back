namespace Library.Application.Interfaces.Repositories
{
    public interface IRepository<TDomainEntity>
    {
        Task<IList<TDomainEntity>> GetManyAsync(int skip, int take);
        Task<TDomainEntity> GetAsync(Guid id);
        Task CreateAsync(TDomainEntity entity);
        Task UpdateAsync(TDomainEntity entity);
        Task DeleteAsync(TDomainEntity entity);
        Task<bool> Exist( Guid id );
    }

}
