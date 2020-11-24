using Mahzan.Dapper.Repositories.Users.ConfirmEmail;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Repositories.Users
{
    public static class ConfirmEmailRepositoryDependency
    {

        public static void Configure(
            IServiceCollection services,
            string connectionString)
        {
            services
                .AddScoped<IConfirmEmailRepository>(
                x => new ConfirmEmailRepository(
                    new NpgsqlConnection(connectionString)
                    ));
        }
    }
}
