using Mahzan.Business.V1.CommandHandlers.Company;
using Mahzan.Business.V1.EventsHandlers.Company.SaveCompany;
using Mahzan.Business.V1.Validations.Company.SaveCompany;
using Mahzan.Dapper.V1.Repositories._Base.Companies;
using Mahzan.Dapper.V1.Repositories.Company.UpdateCompany;
using Microsoft.Extensions.DependencyInjection;

namespace Mahzan.API.Services.Dependencies.CommandsHandlers.Company
{
    public static class CreateCompanyCommandHandlerDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<ICreateCompanyCommandHandler, CreateCompanyCommandHandler>();
        }
    }
}