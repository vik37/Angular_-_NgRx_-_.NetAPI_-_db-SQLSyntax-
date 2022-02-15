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
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medium_Clone_WebAPI.Models.JwtSettingsModel;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            var jwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var seacrets = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(y =>
            {
                y.RequireHttpsMetadata = false;
                y.SaveToken = true;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(seacrets),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IRepository<CurentUser>, UserRepo>();
            services.AddTransient<ICurrentUserService, UserService>();

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _poicyName, builder =>
                 {
                     builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                 });
            });
            services.AddControllers()
                   .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
