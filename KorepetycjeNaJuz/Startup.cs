﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KorepetycjeNaJuz.Configurations;
using KorepetycjeNaJuz.Data;
using KorepetycjeNaJuz.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KorepetycjeNaJuz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KorepetycjeContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            KorepetycjeContext.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            SwaggerConfiguration.RegisterService(services);

            //services.AddIdentity<Users, ApplicationRoles>()
            //    .AddEntityFrameworkStores<KorepetycjeContext>()
            //    .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            SwaggerConfiguration.RegisterUi(app);
        }
    }
}
