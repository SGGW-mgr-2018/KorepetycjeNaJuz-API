using AutoMapper;
using KorepetycjeNaJuz.Configurations;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace KorepetycjeNaJuz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KorepetycjeContext>( options =>
                    options.UseSqlServer( this.Configuration.GetConnectionString( "DefaultConnection" ) ) );
            KorepetycjeContext.ConnectionString = this.Configuration.GetConnectionString( "DefaultConnection" );

            SwaggerConfiguration.RegisterService( services );


            services.AddIdentity<User,IdentityRole<int>>()
            .AddEntityFrameworkStores<KorepetycjeContext>()
            .AddDefaultTokenProviders();

            services.RegisterBearerPolicy( Configuration );            
            
            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
            services.AddAutoMapper(); // Register AutoMapper

            services.AddScoped<IOAuthService, OAuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DatabaseSeed.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            using(KorepetycjeContext context = new KorepetycjeContext())
            {
                context.Database.Migrate();
            }

            app.UseMvc();

            app.UseSwagger();
            SwaggerConfiguration.RegisterUi( app );
        }
    }
}
