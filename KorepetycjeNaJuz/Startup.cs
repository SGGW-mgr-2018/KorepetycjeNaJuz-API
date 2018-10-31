using KorepetycjeNaJuz.Configurations;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KorepetycjeNaJuz
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddDbContext<KorepetycjeContext>( options =>
                    options.UseSqlServer( this.Configuration.GetConnectionString( "DefaultConnection" ) ) );
            KorepetycjeContext.ConnectionString = this.Configuration.GetConnectionString( "DefaultConnection" );
            services.AddScoped<Core.Interfaces.IRepositoryWithTypedId<Lesson, int>, LessonRepository>();
            services.AddScoped<Core.Interfaces.IRepositoryWithTypedId<User, int>, GenericRepository<User>>();

            SwaggerConfiguration.RegisterService( services );


            //services.AddIdentity<Users, IdentityRole>()
            //    .AddEntityFrameworkStores<KorepetycjeContext>()
            //    .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>( options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequiredLength = 8;
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //} );

            //services.AddAuthorization( auth =>
            //{
            //    auth.AddPolicy( "Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme‌​ )
            //        .RequireAuthenticatedUser().Build() );
            //} );

            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseAuthentication();

            using ( KorepetycjeContext context = new KorepetycjeContext() )
            {
                context.Database.Migrate();
            }

            app.UseMvc();

            app.UseSwagger();
            SwaggerConfiguration.RegisterUi( app );
        }
    }
}
