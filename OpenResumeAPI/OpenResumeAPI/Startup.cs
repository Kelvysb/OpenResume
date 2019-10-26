using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenResumeAPI.Business;
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Helpers;
using OpenResumeAPI.Services;
using OpenResumeAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenResumeAPI
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


            IConfiguration config;
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null 
                && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine("Config", "dev.json"), optional: true, reloadOnChange: true)
                    .Build();
            }
            else
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine("Config", "prod.json"), optional: true, reloadOnChange: true)
                    .Build();
            }

            services.Configure<AppSettings>(config);

            var appSettings = config.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

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
                    ValidateAudience = false
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Services
            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IResumeRepository, ResumeRepository>();
            services.AddSingleton<IBlockRepository, BlockRepository>();
            services.AddSingleton<IFieldRepository, FieldRepository>();

            //Business
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IResumeBusiness, ResumeBusiness>();
            services.AddScoped<IBlockBusiness, BlockBusiness>();
            services.AddScoped<IFieldBusiness, FieldBusiness>();

            var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},

                };
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                                    {
                                        Title = "OpenResume API",
                                        Version = "v1",
                                        Description = "OpenResume Back-End API",
                                        Contact = new Contact
                                        {
                                            Name = "Kelvys B.",
                                            Url = "https://github.com/kelvysb"
                                        }
                                    });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });


        }

        private int UserBusiness()
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddFile("Logs/openresume-{Date}.txt");

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenResume API V1");
                options.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
