using Entities;
using Medium_Clone_WebAPI.DataAccess.Interfaces;
using Medium_Clone_WebAPI.DataAccess.Repositories;
using Medium_Clone_WebAPI.Services.interfaces;
using Medium_Clone_WebAPI.Services.MCServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string _poicyName = "CorsePolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            ConnectionString.MCDbConnectionString = Configuration.GetConnectionString("MCConnectionString");
            services.AddTransient<IRepository<CurentUser>, CurrentUserRepo>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _poicyName, builder =>
                 {
                     builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                 });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(_poicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
