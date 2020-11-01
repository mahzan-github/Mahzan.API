using Mahzan.API.Services.Dependencies;
using Mahzan.API.Services.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services
{
    [ExcludeFromCodeCoverage]
    public static class StartupServices
    {
        public static void ConfigureServices(
            this IServiceCollection services,
            string connectionString)
        {
            //Swagger
            SwaggerService.AddSwagger(services);

            //Dependencies
            DependenciesService.AddDependencies(services, connectionString);
        }


    }
}
