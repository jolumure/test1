using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WASiGeMun.Services;
using Entity;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Runtime.InteropServices;

namespace WASiGeMun
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
            //ConnectionString = Configuration.Get<string>("Data:LocalPostgresConection:ConnectionString"); //Jeciel
            ConnectionString = Configuration.Get<string>("Data:LocalPostgresConection:ConnectionStringDevel"); //Jeciel

            string netBase = Path.GetFullPath(Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), @"..\.."));
            Dir_tmp__32 = string.Concat(netBase, @"\Framework\", RuntimeEnvironment.GetSystemVersion(), @"\Temporary ASP.NET Files");
            //Dir_tmp_64 = string.Concat(netBase, @"\Framework64\", RuntimeEnvironment.GetSystemVersion(), @"\Temporary ASP.NET Files");


        }

        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString { get; private set; }//Jeciel
        public static string Dir_tmp__32 { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddCors();
            services.AddMvc();

            services.AddMvcCore()
                   .AddJsonFormatters(a => a.ContractResolver = new CamelCasePropertyNamesContractResolver());

            //Servicios
            services.AddTransient<IWcfServSiGeMun, WcfServSiGeMun>();
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsExceptionTelemetry();
            app.UseMvc();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
