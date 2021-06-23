using FestivalsStub;
using Owin;
using System.Web.Http;

namespace FestivalsStub
{
    public class Startup
    {
        // Configure the Self-Hosted web server.  This is avery simple configuration of web server for this example
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            
            // Configuration for Stubbed Festivals
            config.Routes.MapHttpRoute(
                name: "festivals",
                routeTemplate: "codingtest/api/v1/{controller}",
                defaults: new { }
            );

            // Configuration for data injection - How this is actually implemented would depend on the test environment and execution methodology.  IE. Stub may use
            // Canned 
            config.Routes.MapHttpRoute(
                name: "datainjection",
                routeTemplate: "codingtest/api/v1/{controller}",
                defaults: new { }
            );

            SwaggerConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}
