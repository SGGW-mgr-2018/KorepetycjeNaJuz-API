using KorepetycjeNaJuz.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KorepetycjeNaJuz.Configurations
{
    public static class BearerAuthenticationRegistration
    {
        public static void RegisterBearerPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization( auth =>
            {
                auth.AddPolicy( "Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme )
                    .RequireAuthenticatedUser().Build() );
            } );

            TokenAuthOptions tokenOptions = new TokenAuthOptions
            {
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"],
                LifeSpan = TimeSpan.FromMinutes( Convert.ToDouble( configuration["Jwt:TokenLifespan"] ) ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey( Encoding.UTF8.GetBytes( configuration["Jwt:SecretKey"] ) ),
                    SecurityAlgorithms.HmacSha512Signature )
            };

            services.AddSingleton( tokenOptions );

            services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } )
            .AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = tokenOptions.SigningCredentials.Key,
                    ClockSkew = tokenOptions.LifeSpan

                };
            } );
        }

    }
}
