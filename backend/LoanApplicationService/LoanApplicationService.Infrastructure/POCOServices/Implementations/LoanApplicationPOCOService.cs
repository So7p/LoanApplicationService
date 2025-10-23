using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Domain.POCOs.Enums;
using LoanApplicationService.Infrastructure.Extensions.IQueryable;
using LoanApplicationService.Infrastructure.POCOServices.Abstracts;
using LoanApplicationService.Infrastructure.POCOServices.Implementations.Base;
using LoanApplicationService.Infrastructure.Repositories.Abstracts;
using LoanApplicationService.Infrastructure.Repositories.Abstracts.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoanApplicationService.Infrastructure.POCOServices.Implementations
{
    public class LoanApplicationPOCOService : POCOServiceBase<LoanApplication>, ILoanApplicationPOCOService
    {
        public LoanApplicationPOCOService(IEfRepositoryBase<LoanApplication> efRepositoryBase) : base(efRepositoryBase)
        {

        }

        public Task<TResult[]> GetAsync<TResult>(Expression<Func<LoanApplication, TResult>> selector, IEnumerable<Guid>? ids = null, 
            IEnumerable<LoanStatusEnum>? status = null,
            decimal? minAmount = null,
            decimal? maxAmount = null,
            int? minTermValue = null,
            int? maxTermValue = null)
        {
            return Get(ids) 
                .ExIf(() => status?.Any() == true, q => q.ExFilterByStatus(status))
                .ExIf(() => minAmount is not null || maxAmount is not null, q => q.ExFilterByAmount(minAmount, maxAmount))
                .ExIf(() => minTermValue is not null || maxTermValue is not null, q => q.ExFilterByTermValue(minTermValue, maxTermValue))
                .Select(selector)
                .ToArrayAsync();
        }
    }
}
