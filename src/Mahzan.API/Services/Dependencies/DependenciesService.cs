using Mahzan.API.Services.Dependencies.EventsHandlers.Company;
using Mahzan.API.Services.Dependencies.EventsHandlers.Users;
using Mahzan.API.Services.Dependencies.EventsServices.Email;
using Mahzan.API.Services.Dependencies.Repositories.Members;
using Mahzan.API.Services.Dependencies.Repositories.TaxRegimeCodes;
using Mahzan.API.Services.Dependencies.Repositories.Users;
using Mahzan.API.Services.Dependencies.Rules.Users;
using Mahzan.API.Services.Dependencies.Validations.Company;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Mahzan.API.Services.Dependencies.CommandsHandlers.Company;
using Mahzan.API.Services.Dependencies.Repositories.Company;
using Mahzan.Business.V1.CommandHandlers.Company;

namespace Mahzan.API.Services.Dependencies
{
    [ExcludeFromCodeCoverage]
    public static class DependenciesService
    {
        public static void AddDependencies(
            IServiceCollection services)
        {

            //Repositories
            ConfigureRepositories(services);
            
            //Commands Handlers
            ConfigureCommandHandlers(services);

            //Events Services
            ConfigureEventsServices(services);
        }

        private static void ConfigureRepositories(
            IServiceCollection services)
        {
            // //Users
            // SignUpRepositoryDependency.Configure(services, connectionString);
            // LogInRepositoryDependency.Configure(services, connectionString);
            // ConfirmEmailRepositoryDependency.Configure(services, connectionString);
            //
            //Members
            //MembersRepositoryDependency.Configure(services);
            //
            // //Tax Regime Codes
            // GetTaxRegimeCodesRepositoryDependency.Configure(services, connectionString);
            
            //Company
            CreateCompanyRepositoryDependency.Configure(services);
        }

        private static void ConfigureRules(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpRulesDependency.Configure(services, connectionString);
            LogInRulesDependency.Configure(services, connectionString);
        }

        private static void ConfigureEventsHandlers(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpEventHandlerDependency.Configure(services);
            LoginEventHandlerDependency.Configure(services);

            //Company
            SaveCompanyEventHandlerDependency.Configure(services);
        }

        private static void ConfigureEventsServices(
            IServiceCollection services)
        {
            //Email
            EmailSernderDependency.Configure(services);
        }



        private static void ConfigureCommandHandlers(
            IServiceCollection services)
        {
            CreateCompanyCommandHandlerDependency.Configure(services);
        }

    }


}
