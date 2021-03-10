using Mahzan.Persistance.V1.Repositories.Company;
using Microsoft.Extensions.DependencyInjection;

namespace Mahzan.API.Services.Dependencies.Repositories.Company
{
    public static class CreateCompanyRepositoryDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services.AddScoped<ICreateCompanyRepository, CreateCompanyRepository>();
        }
    }
}