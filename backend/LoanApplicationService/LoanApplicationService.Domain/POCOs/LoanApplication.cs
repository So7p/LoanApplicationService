using LoanApplicationService.Domain.POCOs.Abstracts;
using LoanApplicationService.Domain.POCOs.Enums;

namespace LoanApplicationService.Domain.POCOs
{
    public class LoanApplication : IEntityId
    {
        public Guid Id { get; set; }
        public LoanStatusEnum Status { get; set; }
        public string Number { get; set; } = null!;
        public decimal Amount { get; set; }
        public int TermValue { get; set; }
        public decimal InterestValue { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
