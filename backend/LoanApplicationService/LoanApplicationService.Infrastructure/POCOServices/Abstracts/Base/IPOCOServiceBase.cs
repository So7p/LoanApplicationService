using LoanApplicationService.Domain.POCOs.Abstracts;
using System.Linq.Expressions;

namespace LoanApplicationService.Infrastructure.POCOServices.Abstracts.Base
{
    public interface IPOCOServiceBase<TEntity> where TEntity : IEntityId
    {
        Task<TResult[]> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector, IEnumerable<Guid> ids = null);

        Task<TEntity> AddAsync(TEntity addEntity);

        Task<TEntity> EditAsync(TEntity editEntity);

        Task<TEntity> DeleteAsync(Guid id);
    }
}
