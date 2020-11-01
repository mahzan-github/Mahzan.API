using Mahzan.Dapper.Repositories.Users.SignUp;
using Mahzan.Dapper.Rules.Users.SignUp;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Repositories.Users
{
    public static class SignUpRepositoryDependency
    {
        public static void Configure(
        IServiceCollection services,
        string connectionString)
        {
            services
                .AddScoped<ISignUpRepository>(
                x => new SignUpRepository(
                    new NpgsqlConnection(connectionString),
                    x.GetService<ISignUpRules>()
                    ));
        }
    }
}
