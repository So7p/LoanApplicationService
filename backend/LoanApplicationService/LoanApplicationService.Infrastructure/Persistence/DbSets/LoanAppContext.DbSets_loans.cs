using LoanApplicationService.Domain.POCOs;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationService.Infrastructure.Persistence.DbSets
{
    public partial class LoanAppContext
    {
        public DbSet<LoanApplication> LoanAplications { get; set; }
    }
}
