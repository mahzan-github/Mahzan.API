using Mahzan.API.Services.Dependencies;
using Mahzan.API.Services.Jwt;
using Mahzan.API.Services.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            IConfiguration configuration)
        {
            //Swagger
            SwaggerService.AddSwagger(services);

            //Dependencies
            DependenciesService.AddDependencies(
                services,
                configuration.GetConnectionString("Mahzan")
                );

            //Jwt
            JwtServices.AddJwt(
                services,
                configuration);

        }


    }
}
