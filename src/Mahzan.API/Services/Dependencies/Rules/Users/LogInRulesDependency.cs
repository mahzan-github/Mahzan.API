using Mahzan.Dapper.Rules.Users.LogIn;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Rules.Users
{
    public static class LogInRulesDependency
    {
        public static void Configure(
        IServiceCollection services,
        string connectionString)
        {

            services
                .AddScoped<ILoginRules>(
                x => new LoginRules(
                    new NpgsqlConnection(connectionString)
                    ));

        }
    }
}
