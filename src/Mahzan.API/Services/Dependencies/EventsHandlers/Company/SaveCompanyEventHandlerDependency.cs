using Mahzan.Business.V1.EventsHandlers.Company.SaveCompany;
using Mahzan.Business.V1.Validations.Company.SaveCompany;
using Mahzan.Dapper.V1.Repositories._Base.Companies;
using Mahzan.Dapper.V1.Repositories.Company.CreateCompany;
using Mahzan.Dapper.V1.Repositories.Company.UpdateCompany;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.EventsHandlers.Company
{
    public static class SaveCompanyEventHandlerDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<ISaveCompanyEventHandler>(
                x => new SaveCompanyEventHandler(
                    x.GetService<ICompaniesRepository>(),
                    x.GetService<ICreateCompanyRepository>(),
                    x.GetService<IUpdateCompanyRepository>(),
                    x.GetService<ISaveCompanyValidations>()
                    )
                );
        }
    }
}
