using Mahzan.Dapper.Repositories.Users.Login;
using Mahzan.Dapper.Rules.Users.LogIn;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Repositories.Users
{
    public static class LogInRepositoryDependency
    {
        public static void Configure(
            IServiceCollection services,
            string connectionString)
        {
            services
                .AddScoped<ILoginRepository>(
                x => new LoginRepository(
                    new NpgsqlConnection(connectionString),
                    x.GetService<ILoginRules>()
                    ));
        }
    }
}
