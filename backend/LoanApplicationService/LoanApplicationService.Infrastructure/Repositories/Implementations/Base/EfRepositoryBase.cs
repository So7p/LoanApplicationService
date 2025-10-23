using LoanApplicationService.Infrastructure.Repositories.Abstracts.Base;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationService.Infrastructure.Repositories.Implementations.Base
{
    public abstract class EfRepositoryBase<TEntity> : IEfRepositoryBase<TEntity> where TEntity : class
    {
        public DbContext Context { get; private set; }

        protected EfRepositoryBase(DbContext context)
        {
            this.Context = context;
        }

        public virtual IQueryable<TEntity> Get => Context.Set<TEntity>().AsQueryable();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> EditAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            this.Context.Remove(entity);
            return await Context.SaveChangesAsync();
        }
    }
}
