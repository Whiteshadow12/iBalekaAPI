using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using iBalekaAPI.Services;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using iBalekaAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using iBalekaAPI.Core.Swagger;
using Newtonsoft.Json.Serialization;

namespace iBalekaAPI.Core
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnv = env;

        }

        public IConfigurationRoot Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc()
               .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()) 
               .AddJsonOptions(jsonOptions =>
               {
                   jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               })
               .AddDataAnnotationsLocalization();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "iBaleka API",
                    Description = "iBaleka API for mobile and web inegration",
                    TermsOfService = "None"
                });
                options.OperationFilter<AssignOperationVendorExtensions>();
                options.DescribeAllEnumsAsStrings();
                
            });
            if (_hostingEnv.IsDevelopment())
            {
                services.ConfigureSwaggerGen(c =>
                {
                    c.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
                });
            }
            //services.AddScoped<ISearchProvider, SearchProvider>();
            services.AddDbContext<iBalekaDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ServerConnection")));

            services.AddDistributedMemoryCache();
           
            //repos
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            //services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRunRepository, RunRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services
            services.AddScoped<IAthleteService, AthleteService>();
            services.AddScoped<IClubMemberService, ClubService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IEventRegService, EventRegistrationService>();
            services.AddScoped<IEventService, EventService>();
            //services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRunService, RunService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRouteService, RouteService>();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "api",
                    template: "{controller}/{action}/{id?}");
            });
            app.UseSwagger((httpRequest, swaggerDoc) =>
            {
                swaggerDoc.Host = httpRequest.Host.Value;
            });
            app.UseSwaggerUi();
        }
        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "iBalekaAPI.Core.xml");
        }
    }

}
