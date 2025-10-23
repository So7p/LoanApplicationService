using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Domain.POCOs.Enums;
using LoanApplicationService.Infrastructure.POCOServices.Abstracts.Base;
using System.Linq.Expressions;

namespace LoanApplicationService.Infrastructure.POCOServices.Abstracts
{
    public interface ILoanApplicationPOCOService : IPOCOServiceBase<LoanApplication>
    {
        Task<TResult[]> GetAsync<TResult>(Expression<Func<LoanApplication, TResult>> selector, IEnumerable<Guid>? ids = null, 
            IEnumerable<LoanStatusEnum>? status = null, 
            decimal? minAmount = null, 
            decimal? maxAmount = null,
            int? minTermValue = null, 
            int? maxTermValue = null);
    }
}
