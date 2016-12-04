using System;
using System.Threading.Tasks;

using FluentAssertions;

using HashidsNet;

using NSubstitute;

using Ploeh.AutoFixture;

using Shorter.Api.Services;
using Shorter.Api.Tests.Mocks;
using Shorter.Data;

using Xunit;

namespace Shorter.Api.Tests.Services
{
    public class ShortenerFacts
    {
        private static readonly Fixture Autofixture = new Fixture();
        private ApplicationContext _context;
        private IHashids _hashIdMock;
        private ShortenerService _service;

        public ShortenerFacts()
        {
            _context = MockContextHelper.Create();
            _hashIdMock = Substitute.For<IHashids>();
            _service = new ShortenerService(_context, _hashIdMock);
        }

        [Fact]
        public async Task Creating_using_default_slug_uses_hashId()
        {
            var fakeSlug = Autofixture.Create<string>().Substring(0, 16);
            _hashIdMock.Encode(Arg.Any<int>()).Returns(fakeSlug);

            // Act
            var uri = Autofixture.Create<Uri>();
            var result = await _service.CreateAsync(uri);

            // Assert
            var savedEntity = _context.ShortenedUrls.Find(result);
            savedEntity.Slug.Should().Be(fakeSlug);
        }

        [Fact]
        public async Task Creating_using_custom_slug_slug_should_be_custom()
        {
            var fakeSlug = Autofixture.Create<string>().Substring(0, 16);

            // Act
            var uri = Autofixture.Create<Uri>();
            var result = await _service.CreateAsync(uri, fakeSlug);

            // Assert
            var savedEntity = _context.ShortenedUrls.Find(result);
            savedEntity.Slug.Should().Be(fakeSlug);
        }

        [Fact]
        public async Task Getting_existing_url_should_return_url()
        {
            // Act

            // Assert
        }
    }
}