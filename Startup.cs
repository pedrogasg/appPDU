
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Linq;

using appPDU.Models;
using Microsoft.AspNet.Mvc.Xml;
using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json.Serialization;

namespace appPDU
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration;

        public Startup()
        {
            Configuration = new Configuration().AddJsonFile("config.json");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.OutputFormatters.ToList().RemoveAll(formatter => formatter.Instance is XmlDataContractSerializerOutputFormatter || formatter.Instance is JsonOutputFormatter);

                var jsonOutputFormatter = new JsonOutputFormatter();
                jsonOutputFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                jsonOutputFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;

                options.OutputFormatters.Insert(0, jsonOutputFormatter);
            });
            services.Configure<Settings>(Configuration);
            services.AddSingleton<IObjectModelRepository, ObjectModelRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
