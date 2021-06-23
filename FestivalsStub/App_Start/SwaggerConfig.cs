using System.Web.Http;
using WebActivatorEx;
using FestivalsStub;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace FestivalsStub
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "EA Festivals Test Stub");
            }).EnableSwaggerUi(c => { });
        }
    }
}
