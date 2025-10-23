namespace LoanApplicationService.Infrastructure.Repositories.Abstracts.Base
{
    public interface IEfRepositoryBase<TEntity>
    {
        IQueryable<TEntity> Get { get; }
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> EditAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
