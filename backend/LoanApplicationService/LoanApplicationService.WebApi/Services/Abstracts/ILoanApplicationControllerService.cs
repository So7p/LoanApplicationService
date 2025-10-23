using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Domain.POCOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoanApplicationService.WebApi.Services.Abstracts
{
    public interface ILoanApplicationControllerService
    {
        Task<GetLoanApplicationsResponse> GetAsync(GetLoanApplicationsRequest request);
        Task<LoanApplication> AddAsync(AddLoanApplicationRequest request);
        Task<LoanApplication> EditAsync(EditLoanApplicationRequest request);
        Task<LoanApplication> EditStatusAsync(EditLoanApplicationStatusRequest request);
        Task DeleteAsync(DeleteLoanApplicationRequest request);

        public record GetLoanApplicationsRequest(
            Guid[]? Ids = null,
            LoanStatusEnum[]? status = null,
            decimal? minAmount = null,
            decimal? maxAmount = null,
            int? minTermValue = null,
            int? maxTermValue = null
        );

        public record GetLoanApplicationsResponse(
            IEnumerable<GetLoanApplicationsResponse.LoanApplication> LoanApplications
        )
        {
            public record LoanApplication(Guid Id, LoanStatusEnum Status, string Number, decimal Amount, int TermValue, decimal InterestValue, DateTimeOffset CreatedAt, DateTimeOffset ModifiedAt);
        }

        public record AddLoanApplicationRequest(
            [Required]
            [Range(1, 100000000, ErrorMessage = "Сумма займа минимум 1 у.е, но не более 100.000.000 у.е.")]
            decimal Amount,

            [Required]
            [Range(1, 36, ErrorMessage = "Срок займа минимум 1 месяц, но не более 36 месяцев")]
            int TermValue,

            [Required]
            [Range(0.1, 50, ErrorMessage = "Процентная ставка минимум 0.1%, но не более 50%")]
            decimal InterestValue
        );

        public record EditLoanApplicationRequest(
            [Required]
            Guid Id,

            [Required]
            LoanStatusEnum Status,

            [Required]
            [Range(1, 100000000, ErrorMessage = "Сумма займа минимум 1 у.е, но не более 100.000.000 у.е.")]
            decimal Amount,

            [Required]
            [Range(1, 36, ErrorMessage = "Срок займа минимум 1 месяц, но не более 36 месяцев")]
            int TermValue,

            [Required]
            [Range(0.1, 50, ErrorMessage = "Процентная ставка минимум 0.1%, но не более 50%")]
            decimal InterestValue
        );

        public record EditLoanApplicationStatusRequest(
            [Required]
            Guid Id,

            [Required]
            LoanStatusEnum Status
        );

        public record DeleteLoanApplicationRequest(
            [Required]
            Guid Id
        );
    }
}
