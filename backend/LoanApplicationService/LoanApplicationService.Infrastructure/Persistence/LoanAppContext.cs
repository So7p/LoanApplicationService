using Microsoft.EntityFrameworkCore;

namespace LoanApplicationService.Infrastructure.Persistence
{
    public partial class LoanAppContext : DbContext
    {
        public LoanAppContext(DbContextOptions<LoanAppContext> options) : base(options)
        {
            
        }
    }
}
