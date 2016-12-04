using System;
using System.Web.Http;

using HashidsNet;

using JetBrains.Annotations;

using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

using Owin;

namespace Shorter.Api.Host
{
    public class Startup
    {
        [ThreadStatic] public static IKernel Kernel;

        [UsedImplicitly]
        public void Configuration(IAppBuilder app)
        {
            var kernel = Kernel ?? new StandardKernel();

#if NCRUNCH
            kernel.Load(typeof(Ninject.Web.WebApi.WebApiModule).Assembly);
#endif
            kernel.Bind<IHashids>().To<Hashids>();

            var configuration = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            Boostrap.ConfigureWebApi(configuration);

            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(configuration);
            app.UseErrorPage();
        }
    }
}