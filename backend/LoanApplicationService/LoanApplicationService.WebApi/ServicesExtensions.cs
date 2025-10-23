using LoanApplicationService.Domain.POCOs;
using LoanApplicationService.Infrastructure.POCOServices.Abstracts;
using LoanApplicationService.Infrastructure.POCOServices.Implementations;
using LoanApplicationService.Infrastructure.Repositories.Abstracts;
using LoanApplicationService.Infrastructure.Repositories.Abstracts.Base;
using LoanApplicationService.Infrastructure.Repositories.Implementations;
using LoanApplicationService.WebApi.Services.Abstracts;
using LoanApplicationService.WebApi.Services.Implementations;

namespace LoanApplicationService.WebApi
{
    public static class ServicesExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEfRepositoryBase<LoanApplication>, LoanApplicationRepository>();
            services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoanApplicationPOCOService, LoanApplicationPOCOService>();

            services.AddScoped<ILoanApplicationControllerService, LoanApplicationControllerService>();
        }
    }
}
