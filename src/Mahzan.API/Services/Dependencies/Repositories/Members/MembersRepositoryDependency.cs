using Mahzan.Dapper.V1.Repositories._Base.Members;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.Repositories.Members
{
    public class MembersRepositoryDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<IMembersRepository,MembersRepository>();
        }
    }
}
