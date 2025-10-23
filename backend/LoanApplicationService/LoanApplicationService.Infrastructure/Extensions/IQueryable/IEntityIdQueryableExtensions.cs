using LoanApplicationService.Domain.POCOs.Abstracts;

namespace LoanApplicationService.Infrastructure.Extensions.IQueryable
{
    public static class IEntityIdQueryableExtensions
    {
        public static IQueryable<TEntity> ExFilterById<TEntity>(this IQueryable<TEntity> source, Guid id) where TEntity : IEntityId
        {
            return source.Where(x => x.Id == id);
        }

        public static IQueryable<TEntity> ExFilterByIds<TEntity>(this IQueryable<TEntity> source, IEnumerable<Guid> ids) where TEntity : IEntityId
        {
            return source.Where(x => ids.Contains(x.Id));
        }
    }
}
