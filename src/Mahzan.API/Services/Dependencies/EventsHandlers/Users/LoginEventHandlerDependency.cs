using Mahzan.Business.EventsHandlers.Users.LogIn;
using Mahzan.Dapper.Repositories.Users.Login;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.EventsHandlers.Users
{
    public static class LoginEventHandlerDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<ILoginEventHandler>(
                x => new LoginEventHandler(
                    x.GetService<ILoginRepository>()
                    )
                );
        }
    }
}
