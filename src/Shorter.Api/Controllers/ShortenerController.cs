using System.Threading.Tasks;
using System.Web.Http;

using Shorter.Api.Services;

namespace Shorter.Api.Controllers
{
    [RoutePrefix("")]
    public class ShortenerController : ApiController
    {
        private readonly ShortenerService _service;

        public ShortenerController(ShortenerService service)
        {
            _service = service;
        }

        [Route("{slug}"), HttpGet]
        public async Task<IHttpActionResult> RedirectShortUrl(string slug)
        {
            var shortUrl = await _service.GetUrlBySlugAsync(slug);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl);
        }

        [Route("{slug}/info"), HttpGet]
        public async Task<IHttpActionResult> InfoOfShortUrl(string slug)
        {
            var shortUrl = await _service.GetInfoBySlug(slug);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Ok(shortUrl);
        }
    }
}