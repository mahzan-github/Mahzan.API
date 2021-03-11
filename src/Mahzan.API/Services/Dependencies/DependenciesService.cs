using Mahzan.API.Services.Dependencies.EventsServices.Email;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Mahzan.Business.V1.CommandHandlers.Company;
using Mahzan.Business.V1.CommandHandlers.User;
using Mahzan.Persistance.V1.Repositories.Company;
using Mahzan.Persistance.V1.Repositories.User;

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
            CmmandsHandlers(services);

            //Events Services
            ConfigureEventsServices(services);
        }

        private static void ConfigureRepositories(
            IServiceCollection services)
        {
            services.AddScoped<ICreateCompanyRepository, CreateCompanyRepository>();
            services.AddScoped<ISignUpRepository, SignUpRepository>();
            
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
            
        }

        private static void CmmandsHandlers(
            IServiceCollection services)
        {
            //User
            services.AddScoped<ISignUpCommandHandler, SignUpCommandHandler>();  
            
            //Company
            services.AddScoped<ICreateCompanyCommandHandler, CreateCompanyCommandHandler>();  
        }
        
        private static void ConfigureEventsServices(
            IServiceCollection services)
        {
            //Email
            EmailSernderDependency.Configure(services);
        }
    }


}
