
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Linq;

using appPDU.Models;

namespace appPDU
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IObjectModelRepository,ObjectModelRepository>();
//             services.Configure<MvcOptions>(options =>
//                                                     {
//                                                       options.OutputFormatters.ToList().RemoveAll(formatter => 
//                                                       formatter.Instance is XmlDataContractSerializerOutputFormatter);
//                                                     });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseWelcomePage();
        }
    }
}
