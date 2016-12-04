using System.Web.Http;

namespace Shorter.Api
{
    public static class Boostrap
    {
        public static void ConfigureWebApi(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }
    }
}