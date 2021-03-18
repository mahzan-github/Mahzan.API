using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mahzan.API.Extensions.CORS
{
    internal record CorsExtensionConfiguration
    {
        public string[] AllowedOrigins { get; init; } = System.Array.Empty<string>();
    }

    /// <summary>
    ///     Extension para el manejo de CORS del microservicio
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        ///     Default policy name
        /// </summary>
        public const string CorsDefaultPolicyName = "AllowOriginsHeadersAndMethods";

        /// <summary>
        ///     Agregar Cors al microservicio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsExtensionConfiguration =
                configuration.Get<CorsExtensionConfiguration>() ?? new CorsExtensionConfiguration();
            var allowedOrigins = corsExtensionConfiguration.AllowedOrigins;

            services.AddCors(c =>
            {
                c.AddPolicy(CorsDefaultPolicyName,
                    builder => builder.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }
    }
}