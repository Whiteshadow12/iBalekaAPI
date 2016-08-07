using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Data.Configurations;
using iBalekaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Swashbuckle.Swagger;
using Swashbuckle.SwaggerUi;
using Swashbuckle.SwaggerGen;
using Swashbuckle.Swagger.Model;

namespace iBalekaAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

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
                options.DescribeAllEnumsAsStrings();
            });

            //services.AddScoped<ISearchProvider, SearchProvider>();
            services.AddDbContext<iBalekaDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(i => {
                i.SecurityStampValidationInterval = TimeSpan.FromDays(7);
            })
                .AddEntityFrameworkStores<iBalekaDBContext>()
                .AddDefaultTokenProviders();
            services.AddDistributedMemoryCache();
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            //repos
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<IClubMemberRepository, ClubMemberRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<IEventRegRepository, EventRegistrationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            //services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRunRepository, RunRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services
            services.AddScoped<IAthleteService, AthleteService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IClubMemberService, ClubMemberService>();
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

            app.UseMvc();
        }
    }
}
