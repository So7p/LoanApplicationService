namespace LoanApplicationService.Infrastructure.Extensions.IQueryable
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ExIf<T>(this IQueryable<T> source, Func<bool> checkFunc, Func<IQueryable<T>, IQueryable<T>> filterFunc)
        {
            return checkFunc() ? filterFunc(source) : source;
        }
    }
}
