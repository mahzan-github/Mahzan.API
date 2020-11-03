using Mahzan.Business.EventsServices.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace Mahzan.API.Services.Dependencies.EventsServices.Email
{
    public class EmailSernderDependency
    {
        public static void Configure(
            IServiceCollection services)
        {
            services
                .AddScoped<IEmailSender>(
                x => new EmailSender(
                    x.GetService<IOptions<EmailSettings>>(),
                    x.GetService<IHostingEnvironment>()
                    )
                );
        }
    }
}
