using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Configurations
{
    public class CorsConfiguration
    {
        public const string CorsPolicyName = "Default";

        public static void Register(IServiceCollection services,
            CorsConfigurationValues configuration)
        {
            services?.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder
                        .WithOrigins(configuration.AllowedOrigin)
                        .WithMethods(configuration.AllowedMethods)
                        .WithHeaders(configuration.AllowedHeaders));
            });
        }
    }

}
