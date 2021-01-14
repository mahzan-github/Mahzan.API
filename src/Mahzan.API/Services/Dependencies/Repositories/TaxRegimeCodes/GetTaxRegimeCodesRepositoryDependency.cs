using Mahzan.Dapper.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Repositories.TaxRegimeCodes
{
    public class GetTaxRegimeCodesRepositoryDependency
    {
        public static void Configure(
        IServiceCollection services,
        string connectionString)
        {
            services
                .AddScoped<IGetTaxRegimeCodesRepository>(
                x => new GetTaxRegimeCodesRepository(
                    new NpgsqlConnection(connectionString)
                    ));
        }
    }
}
