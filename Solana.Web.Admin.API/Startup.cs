using AutoMapper;
using Horizon.Common.Custom.Extensions;
using Horizon.Common.Custom.OperationFilters;
using Horizon.Common.Models;
using Horizon.Common.Models.Configurations;
using Horizon.Common.Models.Interfaces;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Master;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Solana.Web.Admin.BLL;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.MappingProfiles;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Solana.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(configuration)
                                .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CustomerCache.Initialize(); //initializes the Solana customer databases
            RegisterDependencies(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Solana Web API Documentation", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.CustomSchemaIds(x => x.FullName);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<SolanaHeadersOperationFilter>();
            });

            services.AddAutoMapper(
                config => config.ValidateInlineMaps = false, //this disables Destination members validation (i.e. Destination members do not all have to be mapped from Source)
                Assembly.GetAssembly(typeof(AdmUserMappingProfile)));

            ConfigureJwtAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            Trace.Listeners.Add(new SerilogTraceListener.SerilogTraceListener());

            app.UseSolanaHeadersMiddleware(); //this sets the Serilog Log Context properties to be able to trace logs more accurately
            app.UseExceptionHandlingMiddleware();

            app.UseHsts();
            
            app.UseHttpsRedirection();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solana Web API Documentation V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            //Logic
            services.AddTransient<IUsersLogic, UsersLogic>();
            services.AddTransient<ISitesLogic, SitesLogic>();
            services.AddTransient<ISiteTypesLogic, SiteTypesLogic>();
            services.AddTransient<IFeedingFiguresLogic, FeedingFiguresLogic>();
            services.AddTransient<ICEPClaimingPercentageLogic, CEPClaimingPercentageLogic>();
            services.AddTransient<ICNImportLogic, CNImportLogic>();
            services.AddTransient<IGlobalOptionsLogic, GlobalOptionsLogic>();
            services.AddTransient<IIntegrationMapsLogic, IntegrationMapsLogic>();
            services.AddTransient<IServingPeriodsLogic, ServingPeriodsLogic>();
            services.AddTransient<IP2ClaimingPercentageLogic, P2ClaimingPercentageLogic>();
            services.AddTransient<IIntegrationJobsLogic, IntegrationJobsLogic>();
            services.AddTransient<IP2ClaimingPercentageLogic, P2ClaimingPercentageLogic>();
            services.AddTransient<ISchoolLogic, SchoolLogic>();
            services.AddTransient<ISchoolGroupsLogic, SchoolGroupsLogic>();
            services.AddTransient<IOnlineApplicationsLogic, OnlineApplicationsLogic>();
            services.AddTransient<IManagementLevelsLogic, ManagementLevelsLogic>();
            services.AddTransient<IUserMessagesLogic, UserMessagesLogic>(); 
            services.AddTransient<IYearEndProcessLogic, YearEndProcessLogic>();

            //Repository
            services.AddScoped<ISolanaRepository>(sr => new SolanaRepository(sr.GetService<ISolanaIdentityUser>().CustomerId, sr.GetService<ISolanaIdentityUser>().AdmUserId));
            services.AddScoped<ISolanaIdentityUser, SolanaIdentityUser>();
        }

        //Consider putting this into an extension method in the Common solution
        private void ConfigureJwtAuth(IServiceCollection services)
        {
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = jwtSettings.ValidateLifetime,

                    ClockSkew = TimeSpan.Zero
                };
            }
            );
        }
    }
}
