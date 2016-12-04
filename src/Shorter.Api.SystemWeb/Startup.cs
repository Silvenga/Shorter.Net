using Microsoft.Owin;

[assembly: OwinStartup(typeof(Shorter.Api.SystemWeb.Startup))]

namespace Shorter.Api.SystemWeb
{
    public class Startup : Host.Startup
    {
    }
}