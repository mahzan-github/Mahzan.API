
using Mahzan.API.Services.Dependencies.EventsHandlers.Users;
using Mahzan.API.Services.Dependencies.Repositories.Users;
using Mahzan.API.Services.Dependencies.Rules.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

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

        }

        private static void ConfigureRepositories(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpRepositoryDependency.Configure(services, connectionString);
        }

        private static void ConfigureRules(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpRulesDependency.Configure(services, connectionString);
        }

        private static void ConfigureEventsHandlers(
            IServiceCollection services,
            string connectionString)
        {
            //Users
            SignUpEventHandlerDependency.Configure(services);
        }


        
    }


}
