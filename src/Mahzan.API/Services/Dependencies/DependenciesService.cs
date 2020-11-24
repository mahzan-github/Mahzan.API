using Mahzan.API.Services.Dependencies.EventsHandlers.Company;
using Mahzan.API.Services.Dependencies.EventsHandlers.Users;
using Mahzan.API.Services.Dependencies.EventsServices.Email;
using Mahzan.API.Services.Dependencies.Repositories.Members;
using Mahzan.API.Services.Dependencies.Repositories.Users;
using Mahzan.API.Services.Dependencies.Rules.Users;
using Mahzan.API.Services.Dependencies.Validations.Company;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Mahzan.API.Services.Dependencies
{
    [ExcludeFromCodeCoverage]
    public static class DependenciesService
    {
        public static void AddDependencies(
            IServiceCollection services,
            string connectionString)
        {
            //Rules
            ConfigureRules(services, connectionString);

            //Repositories
            ConfigureRepositories(services, connectionString);

            //Events Handlers
            ConfigureEventsHandlers(services, connectionString);

            //Events Services
            ConfigureEventsServices(services, connectionString);
        }

        private static void ConfigureRepositories(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpRepositoryDependency.Configure(services, connectionString);
            LogInRepositoryDependency.Configure(services, connectionString);
            ConfirmEmailRepositoryDependency.Configure(services, connectionString);

            //Members
            MembersRepositoryDependency.Configure(services, connectionString);
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
            IServiceCollection services,
            string connectionString)
        {
            //Email
            EmailSernderDependency.Configure(services);
        }

        private static void ConfigureValidations(
            IServiceCollection services) 
        {
            SaveCompanyValidationsDependency.Configure(services);
        }

    }


}
