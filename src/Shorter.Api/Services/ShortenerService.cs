using System;
using System.Data.Entity;
using System.Threading.Tasks;

using HashidsNet;

using JetBrains.Annotations;

using Shorter.Data;
using Shorter.Data.Models;

namespace Shorter.Api.Services
{
    public class ShortenerService : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly IHashids _hashids;

        public ShortenerService(ApplicationContext context, IHashids hashids)
        {
            _context = context;
            _hashids = hashids;
        }

        public async Task<int> CreateAsync(Uri uri, string customSlug = null)
        {
            var shortenedUrl = new ShortenedUrl
            {
                Url = uri.ToString(),
                Slug = customSlug
            };
            _context.ShortenedUrls.Add(shortenedUrl);
            await _context.SaveChangesAsync();

            if (shortenedUrl.Slug == null)
            {
                var id = shortenedUrl.Id;
                var slug = _hashids.Encode(id);
                shortenedUrl.Slug = slug;
                await _context.SaveChangesAsync();
            }

            return shortenedUrl.Id;
        }

        [ItemCanBeNull]
        public async Task<string> GetUrlBySlugAsync(string slug)
        {
            var result = await _context.ShortenedUrls.SingleOrDefaultAsync(x => x.Slug == slug);
            if (result == null)
            {
                return null;
            }
            result.LastUsedOn = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
            return result.Url;
        }

        [ItemCanBeNull]
        public async Task<ShortenedUrlInfoView> GetInfoBySlug(string slug)
        {
            var result = await _context.ShortenedUrls.SingleOrDefaultAsync(x => x.Slug == slug);
            if (result == null)
            {
                return null;
            }
            return new ShortenedUrlInfoView
            {
                Slug = result.Slug,
                Url = result.Url,
                CreatedOn = result.CreatedOn,
                ModifiedOn = result.ModifiedOn,
                LastUsedOn = result.LastUsedOn
            };
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class ShortenedUrlInfoView
    {
        public string Slug { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
        public DateTimeOffset? LastUsedOn { get; set; }
        public string Url { get; set; }
    }
}