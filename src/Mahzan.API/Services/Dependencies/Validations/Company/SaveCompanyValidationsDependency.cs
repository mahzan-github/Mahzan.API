using Mahzan.Business.V1.Validations.Company.SaveCompany;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Validations.Company
{
    public static class SaveCompanyValidationsDependency
    {
        public static void Configure(
            IServiceCollection services)
        {

            services
                .AddScoped<ISaveCompanyValidations>(
                x => new SaveCompanyValidations()
                );

        }
    }
}
