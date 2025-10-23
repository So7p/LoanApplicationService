using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Infrastructure.Persistence;
using LoanApplicationService.Infrastructure.Repositories.Abstracts;
using LoanApplicationService.Infrastructure.Repositories.Implementations.Base;

namespace LoanApplicationService.Infrastructure.Repositories.Implementations
{
    public class LoanApplicationRepository : EfRepositoryBase<LoanApplication>, ILoanApplicationRepository
    {
        public LoanApplicationRepository(LoanAppContext context) :base(context)
        {
            
        }
    }
}
