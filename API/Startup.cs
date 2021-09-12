using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.BAL;
using API.PortalAPI.MotorAPI.HealthBal;
using API.PortalAPI.MotorAPI.MotorBal;
using API.PortalAPI.MotorAPI.TermLifeBal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace API
{
    public class Startup
    {
        public static string Secret = "";
        DbManager.DbManager dbManager;
        public Startup(IConfiguration configuration)
        {
            dbManager = new DbManager.DbManager();
            dbManager.Database.EnsureCreated();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AppDb>(_ => new AppDb(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();
            services.AddSingleton<IBusinessLayer, BusinessLayer>();
            services.AddSingleton<IMotorBusinessLayer, MotorBusinessLayer>();
            services.AddSingleton<IHealthBusinessLayer, HealthBusinessLayer>();
            services.AddSingleton<ITermLifeBusinessLayer, TermLifeBusinessLayer>();

            Secret = Guid.NewGuid().ToString();
            var key = Encoding.ASCII.GetBytes(Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew=TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("cors");
            //app.UseCors(
            //    options => options.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()
            //);
            //app.UseCors(option=>option.AllowAnyOrigin().AllowAnyMethod());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
