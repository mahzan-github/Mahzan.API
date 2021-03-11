﻿using Mahzan.API.Services.Dependencies.EventsServices.Email;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Mahzan.Business.V1.CommandHandlers.Company;
using Mahzan.Business.V1.CommandHandlers.User;
using Mahzan.Business.V1.CommandHandlers.User.LogIn;
using Mahzan.Persistance.V1.Repositories.Company;
using Mahzan.Persistance.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes;
using Mahzan.Persistance.V1.Repositories.User.ConfirmEmail;
using Mahzan.Persistance.V1.Repositories.User.LogIn;
using Mahzan.Persistance.V1.Repositories.User.SignUp;

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
            //TaxRegimeCodes
            services.AddScoped<IGetTaxRegimeCodesRepository, GetTaxRegimeCodesRepository>();
            
            //Company
            services.AddScoped<ICreateCompanyRepository, CreateCompanyRepository>();
            
            //User
            services.AddScoped<ISignUpRepository, SignUpRepository>();
            services.AddScoped<IConfirmEmailRepository, ConfirmEmailRepository>();
            services.AddScoped<ILogInRepository, LogInRepository>();
            
            //Members
            //MembersRepositoryDependency.Configure(services);
            
        }

        private static void CmmandsHandlers(
            IServiceCollection services)
        {
            //User
            services.AddScoped<ISignUpCommandHandler, SignUpCommandHandler>();  
            services.AddScoped<ILogInCommandHandler, LogInCommandHandler>();  
            
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
