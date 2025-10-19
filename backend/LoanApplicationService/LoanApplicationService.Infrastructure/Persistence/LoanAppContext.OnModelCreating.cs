using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationService.Infrastructure.Persistence
{
    public partial class LoanAppContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanApplication>().RegisterLoanApplications();

            base.OnModelCreating(modelBuilder);
        }
    }
}
