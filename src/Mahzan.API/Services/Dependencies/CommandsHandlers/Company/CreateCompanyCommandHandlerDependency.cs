using Mahzan.Business.V1.CommandHandlers.Company;
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