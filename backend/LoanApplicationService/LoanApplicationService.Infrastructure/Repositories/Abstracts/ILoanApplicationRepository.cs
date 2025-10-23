using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Domain.POCOs.Enums;
using LoanApplicationService.Infrastructure.Repositories.Abstracts.Base;

namespace LoanApplicationService.Infrastructure.Repositories.Abstracts
{
    public interface ILoanApplicationRepository : IEfRepositoryBase<LoanApplication>
    {

    }

    public static class LoanApplicationQueryableExtensions
    {
        public static IQueryable<LoanApplication> ExFilterByStatus(this IQueryable<LoanApplication> query, IEnumerable<LoanStatusEnum> status)
        {
            return query.Where(l => status.Contains(l.Status));
        }

        public static IQueryable<LoanApplication> ExFilterByAmount(this IQueryable<LoanApplication> query, decimal? minAmount, decimal? maxAmount)
        {
            if (minAmount.HasValue)
            {
                query = query.Where(l => l.Amount >= minAmount.Value);
            }

            if (maxAmount.HasValue)
            {
                query = query.Where(l => l.Amount <= maxAmount.Value);
            }

            return query;
        }

        public static IQueryable<LoanApplication> ExFilterByTermValue(this IQueryable<LoanApplication> query, int? minTermValue, int? maxTermValue)
        {
            if (minTermValue.HasValue)
            {
                query = query.Where(l => l.TermValue >= minTermValue.Value);
            }

            if (maxTermValue.HasValue)
            {
                query = query.Where(l => l.TermValue <= maxTermValue.Value);
            }

            return query;
        }
    }
}
