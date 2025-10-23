using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using static LoanApplicationService.WebApi.Services.Abstracts.ILoanApplicationControllerService;

namespace LoanApplicationService.WebApi.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationControllerService _loanApplicationControllerService;

        public LoanApplicationController(ILoanApplicationControllerService loanApplicationControllerService)
        {
            _loanApplicationControllerService = loanApplicationControllerService;
        }

        [Route("api/loanapplications/get")]
        [HttpPost]
        public async Task<GetLoanApplicationsResponse> GetAsync([FromBody] GetLoanApplicationsRequest request)
        {
            return await _loanApplicationControllerService.GetAsync(request);
        }

        [Route("api/loanapplications/add")]
        [HttpPost]
        public async Task<LoanApplication> AddAsync([FromBody] AddLoanApplicationRequest request)
        {
            return await _loanApplicationControllerService.AddAsync(request);
        }

        [Route("api/loanapplications/edit")]
        [HttpPost]
        public async Task<LoanApplication> EditAsync([FromBody] EditLoanApplicationRequest request)
        {
            return await _loanApplicationControllerService.EditAsync(request);
        }

        [Route("api/loanapplications/edit/status")]
        [HttpPost]
        public async Task<LoanApplication> EditStatusAsync([FromBody] EditLoanApplicationStatusRequest request)
        {
            return await _loanApplicationControllerService.EditStatusAsync(request);
        }

        [Route("api/loanapplications/delete")]
        [HttpPost]
        public async Task DeleteAsync([FromBody] DeleteLoanApplicationRequest request)
        {
            await _loanApplicationControllerService.DeleteAsync(request);
        }
    }
}
