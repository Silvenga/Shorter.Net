using System;
using System.Net;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Owin.Testing;

using Ninject;

using Ploeh.AutoFixture;

using Shorter.Api.Host.Tests.Mocks;
using Shorter.Data;
using Shorter.Data.Models;

using Xunit;

namespace Shorter.Api.Host.Tests.Controllers
{
    public class ShortenerFacts : IDisposable
    {
        private static readonly Fixture Autofixture = new Fixture();
        private readonly TestServer _server;
        private readonly IKernel _kernel;

        public ShortenerFacts()
        {
            var helper = new MockContextHelper();
            _kernel = new StandardKernel();
            _kernel.Bind<ApplicationContext>().ToMethod(x => helper.Create());
            Startup.Kernel = _kernel;
            _server = TestServer.Create<Startup>();
        }

        [Fact]
        public async Task On_requesting_valid_short_url_should_return_redirect()
        {
            var context = _kernel.Get<ApplicationContext>();

            var shortenedUrl = Autofixture.Build<ShortenedUrl>()
                                           .With(x => x.Url, Autofixture.Create<Uri>().ToString())
                                           .Create();
            context.ShortenedUrls.Add(shortenedUrl);
            context.SaveChanges();

            var request = _server.CreateRequest(shortenedUrl.Slug);

            // Act
            var response = await request.GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.Should().Be(shortenedUrl.Url);
        }

        [Fact]
        public async Task On_requesting_invalid_url_return_404()
        {
            var invalidSlug = Autofixture.Create<string>();
            var request = _server.CreateRequest(invalidSlug);

            // Act
            var response = await request.GetAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}