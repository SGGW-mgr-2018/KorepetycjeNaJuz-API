using AutoMapper;
using KorepetycjeNaJuz.Configurations;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Infrastructure.Auth;
using KorepetycjeNaJuz.Infrastructure.Repositories;
using KorepetycjeNaJuz.Infrastructure.Services;
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
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KorepetycjeContext>(options =>
                    options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            KorepetycjeContext.ConnectionString = this.Configuration.GetConnectionString("DefaultConnection");

            SwaggerConfiguration.RegisterService(services);

            CorsConfiguration.Register(services,
                Configuration.GetSection(nameof(CorsConfigurationValues)).Get<CorsConfigurationValues>());

            services.AddIdentity<User,IdentityRole<int>>()
                    .AddEntityFrameworkStores<KorepetycjeContext>()
                    .AddDefaultTokenProviders();

            services.RegisterBearerPolicy(Configuration);            
            
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(); // Register AutoMapper

            RegisterServices(services);
            RegisterRepositories(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IOAuthService, OAuthService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ILessonSubjectService, LessonSubjectService>();
            services.AddScoped<ICoachLessonService, CoachLessonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonSubjectRepository, LessonSubjectRepository>();
            services.AddScoped<ICoachLessonRepository, CoachLessonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ILessonLevelRepository, LessonLevelRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (KorepetycjeContext context = new KorepetycjeContext())
            {
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
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
            app.UseCors(CorsConfiguration.CorsPolicyName);
            app.UseMvc();

            app.UseSwagger();
            SwaggerConfiguration.RegisterUi(app);
        }
    }
}
