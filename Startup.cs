
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using System.Linq;

using appPDU.Models;
using Microsoft.AspNet.Mvc.Xml;
using Newtonsoft.Json.Serialization;
using appPDU.DataLayer;
using Microsoft.Framework.Configuration;
using System.IO;
using Microsoft.Framework.Runtime;

namespace appPDU
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var ae = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>();
            var jsonConfig = Path.Combine(ae.ApplicationBasePath,"config.json");
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonConfig)
                .AddEnvironmentVariables()
                .Build();
            services
                .AddMvc()
                .AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ObjectModelDbContext>();

            services.ConfigureMvc(options =>
            {
                var settings = options.SerializerSettings;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.Configure<Settings>(Configuration);
            //services.AddSingleton<IObjectModelRepository, ObjectModelRepository>();
            services.AddScoped<IObjectModelRepository<IObjectModel>, ObjectModelEntityRepository>();
            services.AddSingleton<IObjectModelFactory, ObjectModelFactory>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseStaticFiles();
            Newtonsoft.Json.JsonConvert.DefaultSettings = (() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                return settings;
            });
        }
    }
}
