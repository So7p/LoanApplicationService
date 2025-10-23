using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Infrastructure.POCOServices.Abstracts;
using LoanApplicationService.WebApi.Services.Abstracts;
using static LoanApplicationService.WebApi.Services.Abstracts.ILoanApplicationControllerService;

namespace LoanApplicationService.WebApi.Services.Implementations
{
    public class LoanApplicationControllerService : ILoanApplicationControllerService
    {
        private readonly ILoanApplicationPOCOService _loanApplicationPOCOService;

        public LoanApplicationControllerService(ILoanApplicationPOCOService loanApplicationPOCOService)
        {
            _loanApplicationPOCOService = loanApplicationPOCOService;
        }

        public async Task<ILoanApplicationControllerService.GetLoanApplicationsResponse> GetAsync(GetLoanApplicationsRequest request)
        {
            var loanApplications = await _loanApplicationPOCOService.GetAsync(
                selector: l => new ILoanApplicationControllerService.GetLoanApplicationsResponse.LoanApplication(l.Id, l.Status, l.Number, l.Amount, l.TermValue, l.InterestValue, l.CreatedAt, l.ModifiedAt),
                ids: request.Ids,
                status: request.status,
                minAmount: request.minAmount,
                maxAmount: request.maxAmount,
                minTermValue: request.minTermValue,
                maxTermValue: request.maxTermValue
            );

            return new ILoanApplicationControllerService.GetLoanApplicationsResponse(
                LoanApplications: loanApplications
            );
        }

        public async Task<LoanApplication> AddAsync(ILoanApplicationControllerService.AddLoanApplicationRequest request)
        {
            var newLoanApplication = await _loanApplicationPOCOService.AddAsync(new LoanApplication()
            {
                Status = Domain.POCOs.Enums.LoanStatusEnum.Published,
                Number = $"{DateTime.UtcNow:yyyyMMddHHmm}-{Guid.NewGuid()}",
                Amount = request.Amount,
                TermValue = request.TermValue,
                InterestValue = request.InterestValue,
                CreatedAt = DateTimeOffset.UtcNow,
                ModifiedAt = DateTimeOffset.UtcNow
            });

            return newLoanApplication;
        }

        public async Task<LoanApplication> EditAsync(ILoanApplicationControllerService.EditLoanApplicationRequest request)
        {
            var entity = (await _loanApplicationPOCOService
                .GetAsync(
                    selector: l => new ILoanApplicationControllerService.GetLoanApplicationsResponse.LoanApplication(l.Id, l.Status, l.Number, l.Amount, l.TermValue, l.InterestValue, l.CreatedAt, l.ModifiedAt),
                    ids: [request.Id]
                ))
                .SingleOrDefault();

            if (entity == null)
            {
                throw new KeyNotFoundException($"Заявка с Id {request.Id} не найдена");
            }

            var editedEntity = await _loanApplicationPOCOService.EditAsync(new LoanApplication()
            {
                Id = entity.Id,
                Status = request.Status,
                Number = entity.Number,
                Amount = request.Amount,
                TermValue = request.TermValue,
                InterestValue = request.InterestValue,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = DateTimeOffset.UtcNow
            });

            return editedEntity;
        }

        public async Task<LoanApplication> EditStatusAsync(ILoanApplicationControllerService.EditLoanApplicationStatusRequest request)
        {
            var entity = (await _loanApplicationPOCOService
                .GetAsync(
                    selector: l => new ILoanApplicationControllerService.GetLoanApplicationsResponse.LoanApplication(l.Id, l.Status, l.Number, l.Amount, l.TermValue, l.InterestValue, l.CreatedAt, l.ModifiedAt),
                    ids: [request.Id]
                ))
                .SingleOrDefault();

            if (entity == null)
            {
                throw new KeyNotFoundException($"Заявка с Id {request.Id} не найдена");
            }

            var editedEntity = await _loanApplicationPOCOService.EditAsync(new LoanApplication()
            {
                Id = entity.Id,
                Status = request.Status,
                Number = entity.Number,
                Amount = entity.Amount,
                TermValue = entity.TermValue,
                InterestValue = entity.InterestValue,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = DateTimeOffset.UtcNow
            });

            return editedEntity;
        }

        public async Task DeleteAsync(ILoanApplicationControllerService.DeleteLoanApplicationRequest request)
        {
            await _loanApplicationPOCOService.DeleteAsync(request.Id);
        }
    }
}
