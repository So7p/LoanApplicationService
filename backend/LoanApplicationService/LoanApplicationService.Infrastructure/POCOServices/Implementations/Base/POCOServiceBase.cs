using LoanApplicationService.Domain.POCOs.Abstracts;
using LoanApplicationService.Infrastructure.Extensions.IQueryable;
using LoanApplicationService.Infrastructure.POCOServices.Abstracts.Base;
using LoanApplicationService.Infrastructure.Repositories.Abstracts.Base;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationService.Infrastructure.POCOServices.Implementations.Base
{
    public class POCOServiceBase<TEntity> : IPOCOServiceBase<TEntity> where TEntity : class, IEntityId, new()
    {
        private readonly IEfRepositoryBase<TEntity> _efRepositoryBase;

        public POCOServiceBase(IEfRepositoryBase<TEntity> efRepositoryBase)
        {
            _efRepositoryBase = efRepositoryBase;
        }

        protected IQueryable<TEntity> Get(IEnumerable<Guid> ids = null)
        {
            var entity = _efRepositoryBase.Get;

            if (ids is not null)
            {
                entity = entity.ExFilterByIds(ids);
            }

            return entity;
        }

        public async Task<TResult[]> GetAsync<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector, IEnumerable<Guid> ids = null)
        {
            return await Get(ids).AsNoTracking().Select(selector).ToArrayAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity addEntity)
        {
            return await _efRepositoryBase.AddAsync(addEntity);
        }

        public virtual async Task<TEntity> EditAsync(TEntity editEntity)
        {
            return await _efRepositoryBase.EditAsync(editEntity);
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            var entity = await this._efRepositoryBase.Get.ExFilterById(id).AsTracking().SingleOrDefaultAsync();

            if (entity == null)
            {
                throw new KeyNotFoundException($"Заявка с Id {id} не найдена");
            }

            await _efRepositoryBase.DeleteAsync(entity);

            return entity;
        }
    }
}
