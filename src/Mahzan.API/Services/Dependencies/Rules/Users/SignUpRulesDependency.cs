using Mahzan.Dapper.Rules.Users.SignUp;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Rules.Users
{
    public static class SignUpRulesDependency
    {
        public static void Configure(
            IServiceCollection services,
            string connectionString)
        {

            services
                .AddScoped<ISignUpRules>(
                x => new SignUpRules(
                    new NpgsqlConnection(connectionString)
                    ));

        }
    }
}
