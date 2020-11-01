using Mahzan.Business.EventsHandlers.Users.SignUp;
using Mahzan.Dapper.Repositories.Users.SignUp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Services.Dependencies.EventsHandlers.Users
{
    public static class SignUpEventHandlerDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<ISignUpEventHandler>(
                x => new SignUpEventHandler(
                    x.GetService<ISignUpRepository>()
                    )
                );
        }
    }
}
